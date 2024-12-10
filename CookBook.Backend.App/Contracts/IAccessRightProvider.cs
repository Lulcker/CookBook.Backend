namespace CookBook.Backend.App.Contracts;

/// <summary>
/// Провайдер прав доступа
/// </summary>
public interface IAccessRightProvider
{
    /// <summary>
    /// Проверка на зарегистрированного пользователя
    /// </summary>
    void CheckIsCustomer();

    /// <summary>
    /// Проверка на администратора
    /// </summary>
    void CheckIsAdministrator();

    /// <summary>
    /// Проверка на авторизованного пользователя
    /// </summary>
    void CheckIsAuthorized();
}