namespace RIS.Core.Models;

public class Inventory : BaseIdEntity
{
    public string TypeId { get; set; }
    public InventoryType Type { get; set; }
    public string Brand { get; set; }
    public string Size { get; set; }
    public decimal PricePerHour { get; set; }
    public string Status { get; set; } = "Доступен";
    public DateTime LastInspectionDate { get; set; } = DateTime.Now;
    public List<RentalItem> RentalItems { get; set; } = new();
    public List<Maintenance> Maintenances { get; set; } = new();
}