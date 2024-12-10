using CookBook.Backend.Domain.Dictionaries;

namespace CookBook.Backend.Domain.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User : EntityBase
{
    #region Скалярные свойства
    
    /// <summary>
    /// Логин
    /// </summary>
    public required string Login { get; set; }
    
    /// <summary>
    /// Хэш пароля
    /// </summary>
    public required string PasswordHash { get; set; }
    
    /// <summary>
    /// Соль для пароля
    /// </summary>
    public required string Salt { get; set; }

    /// <summary>
    /// Роль
    /// </summary>
    public required UserRole Role { get; set; }

    #endregion

    #region Навигационные свойства

    #region Рецепты

    /// <summary>
    /// Рецепты
    /// </summary>
    public ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();

    #endregion

    #region Избранные рецепты

    /// <summary>
    /// Избранные рецепты
    /// </summary>
    public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; } = new HashSet<FavoriteRecipe>();

    #endregion

    #endregion
}