using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Exceptions.BadRequest;
using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Service.Specifications;
using E_Commerce.Services.Abstraction;
using E_Commerce.Shared.Dtos.Orders;

namespace E_Commerce.Service.Services
{
    public class OrderService(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepository) : IOrderService
    {
        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            //1. Get order address
            var orderAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);

            //2. Get Delivery Method
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(request.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);

            //3. Get Order Items
            var basket = await _basketRepository.GetBasketAsync(request.BasketId);
            if(basket is null) throw new BasketNotFoundException(request.BasketId);

            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                //Check Price 
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                if(product.Price != item.Price)
                    item.Price = product.Price;

                var productInOrderItem = new ProductInOrderItem(item.Id, item.productName, item.PictureUrl);
                var orderItem = new OrderItem(productInOrderItem, item.Price, item.Quantity);

                orderItems.Add(orderItem);
            }

            //4. Calculate Subtotal
            var subTotal = orderItems.Sum(oi => oi.Price * oi.Quantity);

            // Payment Intent
            // Check if order exists
            var spec = new OrderWithPaymentIntentSpecification(basket.PaymentIntentId);

            var existsOrder = await _unitOfWork.GetRepository<Order, Guid>().GetAsync(spec);
            if(existsOrder is not null)
                _unitOfWork.GetRepository<Order, Guid>().Remove(existsOrder);

            //Create order
            var order = new Order(userEmail, orderAddress, deliveryMethod, orderItems, subTotal, basket.PaymentIntentId);

            //Add order to database
            _unitOfWork.GetRepository<Order, Guid>().Add(order);

            var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new CreateOrderBadRequestException();

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliveryMethods);
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrdersForSpecificUserAsync(string userEmail)
        {
            var spec = new OrderSpecification(userEmail);
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);

            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        public async Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail)
        {
            var spec = new OrderSpecification(id, userEmail);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetAsync(spec);
            if (order is null) throw new OrderNotFoundException(id);

            return _mapper.Map<OrderResponse>(order);
        }
    }
}
