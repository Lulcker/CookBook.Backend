using CookBook.Backend.Domain.Dictionaries;

namespace CookBook.Backend.App.Queries.Recipes.Models;

/// <summary>
/// Модель полной информации о рецепте
/// </summary>
public class RecipeFullInfoModel
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
    /// Описание
    /// </summary>
    public required string Description { get; set; }
    
    /// <summary>
    /// Статус
    /// </summary>
    public required RecipeStatus RecipeStatus { get; set; }
    
    /// <summary>
    /// Дата и время создания
    /// </summary>
    public required DateTime CreatedDateTime { get; set; }
    
    /// <summary>
    /// Ссылка на фото
    /// </summary>
    public required string PhotoLink { get; set; }
    
    /// <summary>
    /// Рейтинг
    /// </summary>
    public double Rating { get; set; }
    
    /// <summary>
    /// Название категории
    /// </summary>
    public required string CategoryName { get; set; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public required string UserLogin { get; set; }
    
    /// <summary>
    /// Ингредиенты
    /// </summary>
    public ICollection<IngredientModel> Ingredients { get; set; } = new HashSet<IngredientModel>();

    /// <summary>
    /// Шаги репепта
    /// </summary>
    public ICollection<RecipeStepModel> RecipeSteps { get; set; } = new HashSet<RecipeStepModel>();
    
    /// <summary>
    /// Добавлено в избранное
    /// </summary>
    public bool IsAddedToFavorite { get; set; }
}