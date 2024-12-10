namespace CookBook.Backend.App.Queries.FavoriteRecipes.Models;

/// <summary>
/// Модель избранного рецепта
/// </summary>
public class FavoriteRecipeModel
{
    /// <summary>
    /// Id избранного рецепта
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Id рецепта
    /// </summary>
    public required Guid RecipeId { get; set; }

    /// <summary>
    /// Название рецепта
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Ингридиаенты
    /// </summary>
    public required string Ingredients { get; set; }

    /// <summary>
    /// Ссылка на фото
    /// </summary>
    public required string PhotoLink { get; set; }
    
    /// <summary>
    /// Дата добавления в систему
    /// </summary>
    public required DateTime AddedDateTime { get; set; }
}