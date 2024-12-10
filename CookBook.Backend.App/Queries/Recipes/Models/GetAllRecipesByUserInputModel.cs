using CookBook.Backend.App.Queries.Models;
using CookBook.Backend.Domain.Dictionaries;

namespace CookBook.Backend.App.Queries.Recipes.Models;

/// <summary>
/// Входная модель для получения рецептов для пользователя
/// </summary>
public class GetAllRecipesByUserInputModel : PaginationInputModel
{
    /// <summary>
    /// Статус рецепта
    /// </summary>
    public required RecipeStatus Status { get; set; }
}