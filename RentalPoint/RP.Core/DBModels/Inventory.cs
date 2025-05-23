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
    
    /// <summary>
    ///     Идентификатор типа.
    /// </summary>
    public string TypeId { get; set; }
    
    /// <summary>
    ///     Статус.
    /// </summary>
    public DictionaryValue Status { get; set; } = null!;
    
    /// <summary>
    ///     Идентификатор статуса.
    /// </summary>
    public string StatusId { get; set; }
    
    /// <summary>
    ///     Коллекция связей с заказом.
    /// </summary>
    public ICollection<OrderInventories> OrderInventories { get; set; } = new List<OrderInventories>();
}