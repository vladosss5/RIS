using System.ComponentModel.DataAnnotations;

namespace RP.API.Dtos.Inventory;

public class ReturnInventoryDto
{
    [Required]
    public string InventoryId { get; set; }
}