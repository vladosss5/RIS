using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Core.DBModels;

/// <summary>
///     Тип справочника.
/// </summary>
public class Dictionary : BaseIdEntity
{
    /// <summary>
    ///     Значения справочника данного типа.
    /// </summary>
    public ICollection<DictionaryValue?> DictionaryValues { get; set; } = new Collection<DictionaryValue?>();

    /// <summary>
    ///     Наименование типа справочника.
    /// </summary>
    [Required]
    public string Type { get; set; } = null!;
}