using CookBook.Backend.Domain.Dictionaries;

namespace CookBook.Backend.App.Contracts;

/// <summary>
/// Провайдер для информации о текущем пользователе
/// </summary>
public interface IUserInfoProvider
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    Guid? Id { get; set; }
    
    /// <summary>
    /// Роль пользователя
    /// </summary>
    UserRole Role { get; set; }
    
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    string? Login { get; set; }
}