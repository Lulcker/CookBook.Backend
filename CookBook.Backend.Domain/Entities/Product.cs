namespace CookBook.Backend.Domain.Entities;

/// <summary>
/// Продукт
/// </summary>
public class Product : EntityBase
{
    #region Скалярные свойства

    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }

    #endregion

    #region Навигационные свойства

    #region Продукты и их количество

    /// <summary>
    /// Продукты и их количество
    /// </summary>
    public ICollection<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();

    #endregion

    #endregion
}