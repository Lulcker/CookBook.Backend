using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Backend.App.Commands.Products;

/// <summary>
/// Команда удаления продукта
/// </summary>
public class DeleteProductCommand(
    IRepository<Product> productRepository,
    IChangesSaver changesSaver,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    ILogger<DeleteProductCommand> logger)
{
    public async Task Execute(Guid productId)
    {
        accessRightProvider.CheckIsAdministrator();

        var product = await productRepository
            .Include(p => p.Ingredients)
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (product is null)
            throw new BusinessException("Продукт не найден");

        if (product.Ingredients.Count != 0)
            throw new BusinessException("Нельзя удалить продукт, так как он используется в рецептах");

        await productRepository.RemoveAsync(productId);
        await changesSaver.SaveChangesAsync();
        
        logger.LogInformation("Удалён продукт с Id: {ProductId} администратором {AdministratorName} с Id: {AdministratorId}",
            productId, userInfoProvider.Login, userInfoProvider.Id);
    }
}