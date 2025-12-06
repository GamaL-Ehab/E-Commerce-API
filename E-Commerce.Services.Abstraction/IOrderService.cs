using E_Commerce.Shared.Dtos.Orders;

namespace E_Commerce.Services.Abstraction
{
    public interface IOrderService
    {
        Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail);
        Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync();
        Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid Id, string userEmail);
        Task<IEnumerable<OrderResponse>> GetAllOrdersForSpecificUserAsync(string userEmail);
    }
}
