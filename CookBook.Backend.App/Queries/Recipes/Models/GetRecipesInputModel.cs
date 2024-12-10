using CookBook.Backend.App.Queries.Models;

namespace CookBook.Backend.App.Queries.Recipes.Models;

/// <summary>
/// Входная модель для получения рецептов
/// </summary>
public class GetRecipesInputModel : PaginationInputModel
{
    /// <summary>
    /// Id категории
    /// </summary>
    public Guid? CategoryId { get; set; }
}