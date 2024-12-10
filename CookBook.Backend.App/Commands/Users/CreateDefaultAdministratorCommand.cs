using CookBook.Backend.App.Helpers;
using CookBook.Backend.Domain.Dictionaries;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Backend.App.Commands.Users;

/// <summary>
/// Команда создания дефолтного администратора
/// </summary>
public class CreateDefaultAdministratorCommand(
    IRepository<User> userRepository,
    IChangesSaver changesSaver,
    ILogger<CreateDefaultAdministratorCommand> logger)
{
    #region Consts

    private const string DefaultLogin = "DefaultAdmin";
    private const string DefaultPassword = "Superpass123";

    #endregion
    
    public async Task Execute()
    {
        var defaultAdministrator = await userRepository
            .FirstOrDefaultAsync(u => u.Login == DefaultLogin);
        
        if (defaultAdministrator is not null)
            return;

        var salt = HashHelper.GenerateSalt();
        
        defaultAdministrator = new User
        {
            Login = DefaultLogin,
            PasswordHash = HashHelper.GetHashString(DefaultPassword, salt),
            Salt = salt,
            Role = UserRole.Administrator
        };

        await userRepository.AddAsync(defaultAdministrator);
        await changesSaver.SaveChangesAsync();
        
        logger.LogInformation("Создан администратор по-умолчанию");
    }
}