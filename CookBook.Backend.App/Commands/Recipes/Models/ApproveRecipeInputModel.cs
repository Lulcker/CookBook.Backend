namespace CookBook.Backend.App.Commands.Recipes.Models;

/// <summary>
/// Модель одобрения рецепта
/// </summary>
public class ApproveRecipeInputModel
{
    /// <summary>
    /// Id рецепта
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public required string Description { get; set; }
}