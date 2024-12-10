namespace CookBook.Backend.App.Queries.Recipes.Models;

/// <summary>
/// Модель шага рецепта
/// </summary>
public class RecipeStepModel
{
    /// <summary>
    /// Id
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public required string Description { get; set; }
    
    /// <summary>
    /// Ссылка на фото
    /// </summary>
    public string? PhotoLink { get; set; }
}