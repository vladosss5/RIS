using System.ComponentModel.DataAnnotations;

namespace RIS.Core.Models;

public class BaseIdEntity
{
    [Required]
    public string Id { get; set; }
}