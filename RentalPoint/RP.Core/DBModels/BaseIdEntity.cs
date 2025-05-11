namespace Core.DBModels;

/// <summary>
///     Базовая сущность, содержащая идентификатор.
/// </summary>
public abstract class BaseIdEntity
{
    /// <summary>
    ///     Идентификатор сущности.
    /// </summary>
    public string Id { get; set; } = null!;
}