namespace CookBook.Backend.Domain.Entities;

/// <summary>
/// Категория
/// </summary>
public class Category : EntityBase
{
    #region Скалярные свойства

    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public required string Description { get; set; }

    #endregion

    #region Навигационные свойства

    #region Рецепты

    /// <summary>
    /// Рецепты
    /// </summary>
    public ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();

    #endregion

    #endregion
}