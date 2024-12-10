using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Backend.Controllers;

/// <summary>
/// Контроллер пользователя
/// </summary>
[Authorize]
[ApiController]
[Route("api/user")]
public class UserController : Controller
{
    #region GET

    // Получение всех пользователей
    
    // Получение пользователя по Id

    #endregion

    #region POST
    
    // Создание (Регистрация)

    #endregion

    #region PATCH

    // Блокировка \ разблокировка

    #endregion
}