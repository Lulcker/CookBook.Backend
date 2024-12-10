using CookBook.Backend.App.Commands.Products.Models;
using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Backend.App.Commands.Products;

/// <summary>
/// Команда обновления продукта
/// </summary>
public class UpdateProductCommand(
    IRepository<Product> productRepository,
    IChangesSaver changesSaver,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    ILogger<UpdateProductCommand> logger)
{
    public async Task Execute(UpdateProductInputModel inputModel)
    {
        accessRightProvider.CheckIsAdministrator();
        
        InputModelHelper.TrimStringProperties(inputModel);
        
        if (string.IsNullOrWhiteSpace(inputModel.Name))
            throw new BusinessException("Название продукта не может быть пустым");

        var product = await productRepository
            .FirstOrDefaultAsync(p => p.Name.ToLower() == inputModel.Name.ToLower());

        if (product is not null && product.Id != inputModel.Id)
            throw new BusinessException("Продукт с таким названием уже существует");

        product ??= await productRepository.FirstOrDefaultAsync(p => p.Id == inputModel.Id);

        if (product is null)
            throw new BusinessException("Продукт не найден");

        product.Name = inputModel.Name;

        await changesSaver.SaveChangesAsync();
        
        logger.LogInformation("Обновлён продукт с Id: {ProductId} администратором {AdministratorName} с Id: {AdministratorId}",
            product.Id, userInfoProvider.Login, userInfoProvider.Id);
    }
}