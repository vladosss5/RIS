using Core.DBModels;
using RP.API.Dtos.Order;

namespace RP.API.Mapping;

public class OrderMapper
{
    public Order MapToDomain(OrderCreateDto dto)
    {
        return new Order
        {
            ClientId = dto.ClientId,
            StartDate = DateTime.UtcNow,
            StatusId = "created",
            OrderInventories = dto.InventoryItems.Select(item => new OrderInventories
            {
                InventoryId = item.InventoryId,
                ReturnDateTime = DateTime.UtcNow.AddHours(item.Hours)
            }).ToList()
        };
    }

    public OrderResponseDto MapToDto(Order order)
    {
        return new OrderResponseDto
        {
            Id = order.Id,
            ClientId = order.ClientId,
            Status = order.Status.Value,
            Items = order.OrderInventories.Select(oi => new OrderItemDto
            {
                InventoryId = oi.InventoryId,
                ReturnDate = oi.ReturnDateTime
            }).ToList()
        };
    }
}