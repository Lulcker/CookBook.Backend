using CookBook.Backend.Domain.Dictionaries;

namespace CookBook.Backend.Domain.Entities;

/// <summary>
/// Рецепт
/// </summary>
public class Recipe : EntityBase
{
    #region Скалярные свойства

    /// <summary>
    /// Название
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
    /// Сообщение, почему был отклонён
    /// </summary>
    public string? RejectMessage { get; set; }
    
    /// <summary>
    /// Просмотрено администратором (для показа новых рецептов)
    /// </summary>
    public bool IsAdminViewed { get; set; }
    
    /// <summary>
    /// Просмотрено создателем (для показа новых рецептов)
    /// </summary>
    public bool IsCreatorViewed { get; set; }

    #endregion

    #region Навигационные свойства

    #region Категория

    /// <summary>
    /// Id категории
    /// </summary>
    public required Guid CategoryId { get; set; }

    /// <summary>
    /// Категория
    /// </summary>
    public Category Category { get; set; } = null!;

    #endregion

    #region Продукты и их количество

    /// <summary>
    /// Продукты и их количество
    /// </summary>
    public ICollection<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();

    #endregion

    #region Шаги репепта

    /// <summary>
    /// Шаги репепта
    /// </summary>
    public ICollection<RecipeStep> RecipeSteps { get; set; } = new HashSet<RecipeStep>();

    #endregion

    #region Комментарии

    /// <summary>
    /// Комментарии
    /// </summary>
    public ICollection<RecipeComment> RecipeComments { get; set; } = new HashSet<RecipeComment>();

    #endregion

    #region Пользователь

    /// <summary>
    /// Id пользователя
    /// </summary>
    public required Guid UserId { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; } = null!;

    #endregion
    
    #region Избранные рецепты

    /// <summary>
    /// Избранные рецепты
    /// </summary>
    public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; } = new HashSet<FavoriteRecipe>();

    #endregion

    #endregion
}