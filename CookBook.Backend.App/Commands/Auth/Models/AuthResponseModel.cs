using CookBook.Backend.Domain.Dictionaries;

namespace CookBook.Backend.App.Commands.Auth.Models;

/// <summary>
/// Результат входа в систему
/// </summary>
public class AuthResponseModel
{
    /// <summary>
    /// Jwt токен
    /// </summary>
    public required string JwtToken { get; set; }
    
    /// <summary>
    /// Логин
    /// </summary>
    public required string Login { get; set; }

    /// <summary>
    /// Роль
    /// </summary>
    public required UserRole Role { get; set; }
}