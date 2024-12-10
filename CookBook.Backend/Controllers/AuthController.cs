using CookBook.Backend.App.Commands.Auth;
using CookBook.Backend.App.Commands.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Backend.Controllers;

/// <summary>
/// Контроллер авторизации
/// </summary>
[Authorize]
[ApiController]
[Route("api/auth")]
public class AuthController(
    LoginCommand loginCommand,
    RegistrationCustomerCommand registrationCustomerCommand,
    RegistrationAdministratorCommand registrationAdministratorCommand
    ) : Controller
{
    #region POST

    /// <summary>
    /// Вход в систему
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Результат операции</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthInputModel inputModel)
    {
        return Ok(await loginCommand.Execute(inputModel));
    }
    
    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Результат операции</returns>
    [AllowAnonymous]
    [HttpPost("registration-customer")]
    public async Task<IActionResult> RegistrationCustomer(AuthInputModel inputModel)
    {
        return Ok(await registrationCustomerCommand.Execute(inputModel));
    }
    
    /// <summary>
    /// Регистрация нового администратора
    /// </summary>
    /// <param name="inputModel">Входная модель</param>
    /// <returns>Результат операции</returns>
    [HttpPost("registration-administrator")]
    public async Task<IActionResult> RegistrationAdministrator(AuthInputModel inputModel)
    {
        await registrationAdministratorCommand.Execute(inputModel);
        return Ok();
    }

    #endregion
}