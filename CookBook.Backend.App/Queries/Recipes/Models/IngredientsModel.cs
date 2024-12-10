using CookBook.Backend.Domain.Dictionaries;

namespace CookBook.Backend.App.Queries.Recipes.Models;

/// <summary>
/// Модель ингредиента
/// </summary>
public class IngredientsModel
{
    /// <summary>
    /// Id
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Продукт
    /// </summary>
    public required string ProductName { get; set; }
    
    /// <summary>
    /// Количество
    /// </summary>
    public required double Quantity { get; set; }
    
    /// <summary>
    /// Единица измерения
    /// </summary>
    public required UnitOfMeasure UnitOfMeasure { get; set; }
}