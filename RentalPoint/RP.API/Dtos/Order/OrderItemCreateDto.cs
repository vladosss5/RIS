using System.ComponentModel.DataAnnotations;

namespace RP.API.Dtos.Order;

public class OrderItemCreateDto
{
    [Required]
    public string InventoryId { get; set; }

    [Range(1, 24, ErrorMessage = "Аренда возможна от 1 до 24 часов")]
    public int Hours { get; set; }
}