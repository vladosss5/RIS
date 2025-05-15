namespace RP.API.Dtos.Order;

public class OrderResponseDto
{
    public string Id { get; set; }
    public DateTime StartDate { get; set; }
    public string Status { get; set; }
    public decimal? TotalPrice { get; set; }
    public string InventoryId { get; set; }
}