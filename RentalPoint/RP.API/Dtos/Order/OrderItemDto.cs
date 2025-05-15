namespace RP.API.Dtos.Order;

/// <summary>
/// DTO для элемента заказа (арендованный инвентарь)
/// </summary>
public class OrderItemDto
{
    /// <summary>
    /// ID инвентаря
    /// </summary>
    public string InventoryId { get; set; }

    /// <summary>
    /// Название инвентаря (если нужно)
    /// </summary>
    public string? InventoryName { get; set; }

    /// <summary>
    /// Дата и время возврата
    /// </summary>
    public DateTime ReturnDate { get; set; }

    /// <summary>
    /// Стоимость аренды за час
    /// </summary>
    public decimal PricePerHour { get; set; }
}