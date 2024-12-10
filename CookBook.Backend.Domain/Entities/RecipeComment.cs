namespace CookBook.Backend.Domain.Entities;

public class RecipeComment : EntityBase
{
    #region Скалярные свойства

    /// <summary>
    /// Рейтинг
    /// </summary>
    public required int Rating { get; set; }
    
    /// <summary>
    /// Комментарий
    /// </summary>
    public required string Comment { get; set; }

    #endregion

    #region Навигационные свойства

    #region Рецепт

    /// <summary>
    /// Id рецепта
    /// </summary>
    public required Guid RecipeId { get; set; }

    /// <summary>
    /// Рецепт
    /// </summary>
    public Recipe Recipe { get; set; } = null!;

    #endregion

    #endregion
}