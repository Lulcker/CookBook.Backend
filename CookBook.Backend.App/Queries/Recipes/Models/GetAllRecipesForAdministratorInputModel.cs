using CookBook.Backend.App.Queries.Models;
using CookBook.Backend.Domain.Dictionaries;

namespace CookBook.Backend.App.Queries.Recipes.Models;

/// <summary>
/// Входная модель для получения рецептов для администратора
/// </summary>
public class GetAllRecipesForAdministratorInputModel : PaginationInputModel
{
    public required RecipeStatus Status { get; set; }
}