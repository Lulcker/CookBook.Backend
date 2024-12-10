namespace CookBook.Backend.App.Commands.Recipes.Models;

/// <summary>
/// Входная модель отклонения рецепта
/// </summary>
public class RejectRecipeInputModel
{
    /// <summary>
    /// Id рецепта
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Комментарий
    /// </summary>
    public required string Comment { get; set; }
}