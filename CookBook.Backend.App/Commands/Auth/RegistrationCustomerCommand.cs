using CookBook.Backend.App.Commands.Auth.Models;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.App.Rules;
using CookBook.Backend.Domain.Dictionaries;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Backend.App.Commands.Auth;

/// <summary>
/// Команда регистрации нового пользователя
/// </summary>
public class RegistrationCustomerCommand(
    IRepository<User> userRepository,
    IChangesSaver changesSaver,
    CreateJwtTokenRule createJwtTokenRule,
    ILogger<RegistrationCustomerCommand> logger)
{
    public async Task<AuthResponseModel> Execute(AuthInputModel inputModel)
    {
        if (string.IsNullOrWhiteSpace(inputModel.Login))
            throw new BusinessException("Логин не может быть пустым");
        
        if (string.IsNullOrWhiteSpace(inputModel.Password))
            throw new BusinessException("Пароль не может быть пустым");
        
        var user = await userRepository
            .FirstOrDefaultAsync(u => u.Login == inputModel.Login);

        if (user is not null)
            throw new BusinessException("Пользователь с таким логином уже существует");

        var salt = HashHelper.GenerateSalt();
        
        user = new User
        {
            Login = inputModel.Login,
            PasswordHash = HashHelper.GetHashString(inputModel.Password, salt),
            Salt = salt,
            Role = UserRole.Customer
        };

        await userRepository.AddAsync(user);
        await changesSaver.SaveChangesAsync();
        
        logger.LogInformation("Создан новый пользователь с логином {Login}, Id: {UserId}",
            user.Login, user.Id);

        return new AuthResponseModel
        {
            JwtToken = createJwtTokenRule.Execute(user),
            Login = user.Login,
            Role = user.Role
        };
    }
}