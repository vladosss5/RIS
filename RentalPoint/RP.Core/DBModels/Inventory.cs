namespace Core.DBModels;

/// <summary>
///     Инвентарь.
/// </summary>
public class Inventory : BaseIdEntity
{
    /// <summary>
    ///     Название.
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    ///     Стоимость за час.
    /// </summary>
    public decimal PricePerHour { get; set; }
    
    /// <summary>
    ///     Тип инвентаря.
    /// </summary>
    public DictionaryValue Type { get; set; } = null!;
    
    public string TypeId { get; set; }
}