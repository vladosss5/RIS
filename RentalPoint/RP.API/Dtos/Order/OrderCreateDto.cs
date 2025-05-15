namespace RP.API.Dtos.Order;

public class OrderCreateDto
{
    public string ClientId { get; set; }
    public List<OrderItemCreateDto> InventoryItems { get; set; }
    public string? DepositId { get; set; }
}