using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DBModels;

/// <summary>
///     Значение справочника.
/// </summary>
public class DictionaryValue : BaseIdEntity
{
    /// <summary>
    ///     Тип справочника.
    /// </summary>
    [Required]
    public Dictionary Dictionary { get; set; } = null!;
    
    public string DictionaryId { get; set; }

    /// <summary>
    ///     Значение справочника.
    /// </summary>
    [Required]
    public string Value { get; set; } = null!;
}