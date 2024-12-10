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
/// Команда создания продукта
/// </summary>
public class CreateProductCommand(
    IRepository<Product> productRepository,
    IChangesSaver changesSaver,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    ILogger<CreateProductCommand> logger)
{
    public async Task Execute(CreateProductInputModel inputModel)
    {
        accessRightProvider.CheckIsAdministrator();
        
        InputModelHelper.TrimStringProperties(inputModel);
        
        if (string.IsNullOrWhiteSpace(inputModel.Name))
            throw new BusinessException("Название продукта не может быть пустым");

        var product = await productRepository
            .FirstOrDefaultAsync(p => p.Name.ToLower() == inputModel.Name.ToLower());

        if (product is not null)
            throw new BusinessException("Продукт с таким названием уже существует");

        product = new Product
        {
            Name = inputModel.Name
        };

        await productRepository.AddAsync(product);
        await changesSaver.SaveChangesAsync();
        
        logger.LogInformation("Создан продукт {ProductName} администратором {AdministratorName} с Id: {AdministratorId}",
            product.Name, userInfoProvider.Login, userInfoProvider.Id);
    }
}