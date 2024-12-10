namespace CookBook.Backend.Domain.Entities;

/// <summary>
/// Избранный рецепт
/// </summary>
public class FavoriteRecipe : EntityBase
{
    #region Скалярные свойства

    /// <summary>
    /// Дата добавления в систему
    /// </summary>
    public required DateTime AddedDateTime { get; set; }

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
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public required Guid UserId { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; } = null!;

    #endregion
}