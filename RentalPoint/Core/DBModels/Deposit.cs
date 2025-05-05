namespace Core.DBModels;

/// <summary>
///     Залог.
/// </summary>
public class Deposit : BaseIdEntity
{
    /// <summary>
    ///     Номер документа.
    /// </summary>
    public string DocumentNumber { get; set; } = null!;
    
    /// <summary>
    ///     Тип документа.
    /// </summary>
    public DictionaryValue Type { get; set; } = null!;
    
    /// <summary>
    ///     Навигационное св-во заказов.
    /// </summary>
    public ICollection<Order>? OrderLinks { get; set; }
}