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
            OrderInventories = dto.InventoryIds.Select(x => new OrderInventories{ InventoryId = x }).ToList(),
            DepositId = dto.DepositId
        };
    }

    public OrderResponseDto MapToDto(Order order)
    {
        return new OrderResponseDto
        {
            Id = order.Id,
            StartDate = order.StartDate,
            Status = order.Status.ToString(),
            TotalPrice = order.FullPrice,
            InventoryId = order.InventoryId
        };
    }
}