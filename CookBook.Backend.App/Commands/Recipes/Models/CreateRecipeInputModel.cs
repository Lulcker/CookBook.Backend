namespace CookBook.Backend.App.Commands.Recipes.Models;

/// <summary>
/// Входная модель для создания рецепта
/// </summary>
public class CreateRecipeInputModel
{
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public required string Description { get; set; }
    
    /// <summary>
    /// Ссылка на фото
    /// </summary>
    public required string PhotoLink { get; set; }
    
    /// <summary>
    /// Id категории
    /// </summary>
    public required Guid CategoryId { get; set; }
    
    /// <summary>
    /// Шаги репепта
    /// </summary>
    public required ICollection<CreateRecipeRecipeStepInputModel> RecipeSteps { get; set; }
    
    /// <summary>
    /// Ингредиенты
    /// </summary>
    public required ICollection<CreateRecipeIngredientsInputModel> Ingredients { get; set; }
}