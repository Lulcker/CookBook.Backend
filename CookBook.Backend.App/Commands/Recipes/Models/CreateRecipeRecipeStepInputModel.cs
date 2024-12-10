namespace CookBook.Backend.App.Commands.Recipes.Models;

/// <summary>
/// Входная модель для шагов рецепта
/// </summary>
public class CreateRecipeRecipeStepInputModel
{
    /// <summary>
    /// Описание
    /// </summary>
    public required string Description { get; set; }
    
    /// <summary>
    /// Ссылка на фото
    /// </summary>
    public string? PhotoLink { get; set; }
}