using Core.DBModels;

namespace RP.Application.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Order order);
    Task<Order?> GetOrderByIdAsync(string id);
    Task ReturnInventoryAsync(string orderId, string inventoryId);
}