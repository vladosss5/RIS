namespace RIS.Core.Models;

public class Maintenance : BaseIdEntity
{
    public string InventoryId { get; set; }
    public Inventory Inventory { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } // "Плановая проверка", "Починка колеса"
    public bool IsPlanned { get; set; } // true = плановый, false = внеплановый
}