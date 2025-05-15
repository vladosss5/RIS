namespace RP.API.Dtos.Inventory;

public class InventoryCreateDto
{
    public string Name { get; set; }
    
    public decimal PricePerHour { get; set; }
    
    public string TypeId { get; set; }
    
    public string StatusId { get; set; }
}