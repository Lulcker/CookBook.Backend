namespace CookBook.Backend.App.Queries.Recipes.Models;

/// <summary>
/// Модель рецепта
/// </summary>
public class RecipeModel
{
    /// <summary>
    /// Id рецепта
    /// </summary>
    public required Guid Id { get; set; }

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
    /// Рейтинг
    /// </summary>
    public double Rating { get; set; }
    
    /// <summary>
    /// Дата и время создания
    /// </summary>
    public required DateTime CreatedDateTime { get; set; }

    /// <summary>
    /// Добавлено в избранное
    /// </summary>
    public bool IsAddedToFavorite { get; set; }
}