namespace CookBook.Backend.Domain.Entities;

/// <summary>
/// Шаг рецепта
/// </summary>
public class RecipeStep : EntityBase
{
    #region Скалярные свойства

    /// <summary>
    /// Описание
    /// </summary>
    public required string Description { get; set; }
    
    /// <summary>
    /// Ссылка на фото
    /// </summary>
    public string? PhotoLink { get; set; }

    #endregion

    #region Навигационные свойства

    /// <summary>
    /// Id рецепта
    /// </summary>
    public required Guid RecipeId { get; set; }

    /// <summary>
    /// Рецепт
    /// </summary>
    public Recipe Recipe { get; set; } = null!;

    #endregion
}