namespace Core.DBModels;

/// <summary>
///     Клиент.
/// </summary>
public class Client : BaseIdEntity
{
    /// <summary>
    ///     Имя.
    /// </summary>
    public string FName { get; set; } = null!;

    /// <summary>
    ///     Фамилия.
    /// </summary>
    public string SName { get; set; } = null!;
    
    /// <summary>
    ///     Отчество.
    /// </summary>
    public string? LName { get; set; }
    
    /// <summary>
    ///     Номер телефона.
    /// </summary>
    public string PhoneNumber { get; set; } = null!;
    
    /// <summary>
    ///     Навигационное св-во заказов.
    /// </summary>
    public ICollection<Order>? OrderLinks { get; set; }
}