using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.Domain.Dictionaries;

namespace CookBook.Backend.Infrastructure.Providers;

/// <summary>
/// Провайдер прав доступа
/// </summary>
public class AccessRightProvider(IUserInfoProvider userInfoProvider) : IAccessRightProvider
{
    /// <summary>
    /// Проверка на зарегистрированного пользователя
    /// </summary>
    public void CheckIsCustomer()
    {
        if (userInfoProvider.Role is not UserRole.Customer)
            throw new AccessDeniedException("Недостаточно прав");
    }

    /// <summary>
    /// Проверка на администратора
    /// </summary>
    public void CheckIsAdministrator()
    {
        if (userInfoProvider.Role is not UserRole.Administrator)
            throw new AccessDeniedException("Недостаточно прав");
    }

    /// <summary>
    /// Проверка на авторизованного пользователя
    /// </summary>
    public void CheckIsAuthorized()
    {
        if (userInfoProvider.Role is not (UserRole.Customer or UserRole.Administrator))
            throw new AccessDeniedException("Авторизуйтесь для продолжения");
    }
}