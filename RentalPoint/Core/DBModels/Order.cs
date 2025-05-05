namespace Core.DBModels;

/// <summary>
///     Заказ.
/// </summary>
public class Order : BaseIdEntity
{
    /// <summary>
    ///     Начало оформления заказа.
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    ///     Полная стоимость.
    /// </summary>
    public decimal? FullPrice { get; set; }
    
    /// <summary>
    ///     Клиент.
    /// </summary>
    public Client? Client { get; set; }
    
    /// <summary>
    ///     Статус.
    /// </summary>
    public DictionaryValue Status { get; set; } = null!;
    
    /// <summary>
    ///     Залог.
    /// </summary>
    public Deposit? Deposit { get; set; }
}