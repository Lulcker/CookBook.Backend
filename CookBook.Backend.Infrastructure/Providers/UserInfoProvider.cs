using CookBook.Backend.App.Contracts;
using CookBook.Backend.Domain.Dictionaries;

namespace CookBook.Backend.Infrastructure.Providers;

/// <summary>
/// Провайдер для информации о текущем пользователе
/// </summary>
public class UserInfoProvider : IUserInfoProvider
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid? Id { get; set; }
    
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public UserRole Role { get; set; }
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string? Login { get; set; }
}