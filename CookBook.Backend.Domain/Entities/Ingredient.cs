using CookBook.Backend.Domain.Dictionaries;

namespace CookBook.Backend.Domain.Entities;

/// <summary>
/// Количество продукта
/// </summary>
public class Ingredient : EntityBase
{
    #region Скалярные свойства

    /// <summary>
    /// Количество
    /// </summary>
    public required double Quantity { get; set; }
    
    /// <summary>
    /// Единица измерения
    /// </summary>
    public required UnitOfMeasure UnitOfMeasure { get; set; }

    #endregion

    #region Навигационные свойства

    #region Продукт

    /// <summary>
    /// Id
    /// </summary>
    public required Guid ProductId { get; set; }

    /// <summary>
    /// Продукт
    /// </summary>
    public Product Product { get; set; } = null!;

    #endregion

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