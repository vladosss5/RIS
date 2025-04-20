namespace RIS.Core.Models;

public class RentalItem : BaseIdEntity
{
    public string RentalId { get; set; }
    public Rental Rental { get; set; }
    public string InventoryId { get; set; }
    public Inventory Inventory { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal ActualCost { get; set; }
}