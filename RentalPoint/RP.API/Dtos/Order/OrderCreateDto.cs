namespace RP.API.Dtos.Order;

public class OrderCreateDto
{
    public string ClienId { get; set; }
    
    public string DepositId { get; set; }
    
    public List<string> InventoryIds { get; set; }
}