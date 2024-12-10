namespace CookBook.Backend.Domain.Dictionaries;

/// <summary>
/// Роли пользователей
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Анонимный пользователь
    /// </summary>
    Anonymous,
    
    /// <summary>
    /// Зарегистрированный пользователь
    /// </summary>
    Customer,
    
    /// <summary>
    /// Администратор
    /// </summary>
    Administrator
}