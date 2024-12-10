namespace CookBook.Backend.App.Commands.Auth.Models;

/// <summary>
/// Входная модель входа в систему
/// </summary>
public class AuthInputModel
{
    /// <summary>
    /// Логин
    /// </summary>
    public required string Login { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; set; }
}