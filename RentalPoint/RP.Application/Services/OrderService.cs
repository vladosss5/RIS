using Core.DBModels;
using Data;
using Microsoft.EntityFrameworkCore;
using RP.Application.Interfaces;

namespace RP.Application.Services;

public class OrderService : IOrderService
{
    private readonly DataContext _context;

    public OrderService(DataContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public Task<Order?> GetOrderByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task ReturnInventoryAsync(string orderId, string inventoryId)
    {
        var orderItem = await _context.OrderInventories
            .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.InventoryId == inventoryId);

        if (orderItem != null)
        {
            orderItem.ReturnDateTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}