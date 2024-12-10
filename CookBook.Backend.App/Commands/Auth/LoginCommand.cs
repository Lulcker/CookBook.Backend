using CookBook.Backend.App.Commands.Auth.Models;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.App.Rules;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Backend.App.Commands.Auth;

/// <summary>
/// Команда входа в систему
/// </summary>
public class LoginCommand(
    IRepository<User> userRepository,
    CreateJwtTokenRule createJwtTokenRule,
    ILogger<LoginCommand> logger)
{
    public async Task<AuthResponseModel> Execute(AuthInputModel inputModel)
    {
        if (string.IsNullOrWhiteSpace(inputModel.Login))
            throw new BusinessException("Логин не может быть пустым");
        
        if (string.IsNullOrWhiteSpace(inputModel.Password))
            throw new BusinessException("Пароль не может быть пустым");
        
        if (inputModel.Password.Length < 8)
            throw new BusinessException("Длина пароля не может быть менее 8 символов");
        
        var user = await userRepository
            .FirstOrDefaultAsync(u => u.Login == inputModel.Login);

        if (user is null)
            throw new BusinessException("Пользователя с таким логином не существует");

        if (user.PasswordHash != HashHelper.GetHashString(inputModel.Password, user.Salt))
            throw new BusinessException("Неверный пароль");
        
        logger.LogInformation("Пользователь под логином {Login} выполнил вход в систему", user.Login);

        return new AuthResponseModel
        {
            JwtToken = createJwtTokenRule.Execute(user),
            Login = user.Login,
            Role = user.Role
        };
    }
}