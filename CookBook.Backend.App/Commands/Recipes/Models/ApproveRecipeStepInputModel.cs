namespace CookBook.Backend.App.Commands.Recipes.Models;

/// <summary>
/// Входная модель шагов при одобрении рецепта
/// </summary>
public class ApproveRecipeStepInputModel
{
    /// <summary>
    /// Id шага
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public required string Description { get; set; }
}