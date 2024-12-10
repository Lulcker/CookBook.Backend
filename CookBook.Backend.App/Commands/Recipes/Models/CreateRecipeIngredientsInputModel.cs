using CookBook.Backend.Domain.Dictionaries;

namespace CookBook.Backend.App.Commands.Recipes.Models;

/// <summary>
/// Входная модель для ингредиентов при создании рецепта
/// </summary>
public class CreateRecipeIngredientsInputModel
{
    /// <summary>
    /// Количество
    /// </summary>
    public required double Quantity { get; set; }
    
    /// <summary>
    /// Единица измерения
    /// </summary>
    public required UnitOfMeasure UnitOfMeasure { get; set; }

    /// <summary>
    /// Id продукта
    /// </summary>
    public required Guid ProductId { get; set; }
}