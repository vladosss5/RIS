namespace RP.API.Dtos.Order;

public class OrderResponseDto
{
    public string Id { get; set; }
    public string ClientId { get; set; }
    public string Status { get; set; }
    public List<OrderItemDto> Items { get; set; }
}