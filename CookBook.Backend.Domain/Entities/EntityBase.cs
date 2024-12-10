namespace CookBook.Backend.Domain.Entities;

/// <summary>
/// Базовая сущность
/// </summary>
public class EntityBase
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
}