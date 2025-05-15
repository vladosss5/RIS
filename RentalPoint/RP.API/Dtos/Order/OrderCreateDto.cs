namespace RP.API.Dtos.Order;

public class OrderCreateDto
{
    public string ClientId { get; set; }
    public List<string> InventoryIds { get; set; }
    public string? DepositId { get; set; }
}