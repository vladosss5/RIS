namespace RIS.Core.Models;

public class InventoryType : BaseIdEntity
{
    public string Name { get; set; }
    public List<Inventory> Inventory { get; set; } = new();
}