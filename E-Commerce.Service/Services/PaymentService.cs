using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Services.Abstraction;
using E_Commerce.Shared.Dtos.Baskets;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = E_Commerce.Domain.Entities.Products.Product;

namespace E_Commerce.Service.Services
{
    public class PaymentService(IBasketRepository _basketRepository, IUnitOfWork _unitOfWork, IConfiguration _configuration, IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreatePaymentIntentAsync(string basketId)
        {
            //1. Calculate Total Cost(Amount) = SubTotal + delivery Method Cost 

            // Get Basket By Id
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) throw new BasketNotFoundException(basketId);

            // Check Product and Its Price
            foreach(var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;
            }

            // Calculate Subtotal
            var subTotal = basket.Items.Sum(i => i.Price * i.Quantity);

            // Get Delivery Method By Id
            if (!basket.DeliveryMethodId.HasValue) throw new DeliveryMethodNotFoundException(-1);

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
            if(deliveryMethod is null) throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

            basket.ShippingCost = deliveryMethod.Price;
            var amount = subTotal + deliveryMethod.Price;

            //Send Amount to Stripe
            StripeConfiguration.ApiKey = _configuration["StripeOptions:SecretKey"];

            PaymentIntentService paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if(basket.PaymentIntentId is null)
            {
                // Create new payment intent id
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)amount * 100,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);
            }
            else
            {
                // Update payment intent id
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)amount * 100
                };

                paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
            }

            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;

            basket = await _basketRepository.CreateBasketAsync(basket, TimeSpan.FromDays(1));

            return _mapper.Map<BasketDto>(basket);
        }
    }
}
