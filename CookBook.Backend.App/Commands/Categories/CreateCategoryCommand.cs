using CookBook.Backend.App.Commands.Categories.Models;
using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Backend.App.Commands.Categories;

/// <summary>
/// Команда создания категории
/// </summary>
public class CreateCategoryCommand(
    IRepository<Category> categoryRepository,
    IChangesSaver changesSaver,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    ILogger<CreateCategoryCommand> logger
    )
{
    public async Task Execute(CreateCategoryInputModel inputModel)
    {
        accessRightProvider.CheckIsAdministrator();
        
        InputModelHelper.TrimStringProperties(inputModel);

        if (string.IsNullOrWhiteSpace(inputModel.Name))
            throw new BusinessException("Название категории не может быть пустым");
        
        if (string.IsNullOrWhiteSpace(inputModel.Description))
            throw new BusinessException("Описание категории не может быть пустым");
        
        var category =
            await categoryRepository.FirstOrDefaultAsync(c =>
                c.Name.ToLower() == inputModel.Name.ToLower());

        if (category is not null)
            throw new BusinessException("Категория с таким названием уже существует");

        category = new Category
        {
            Name = inputModel.Name,
            Description = inputModel.Description
        };

        await categoryRepository.AddAsync(category);
        await changesSaver.SaveChangesAsync();
        
        logger.LogInformation("Добавлена категория {CategoryName} администратором {AdministratorName} c Id: {AdministratorId}", 
            inputModel.Name, userInfoProvider.Login, userInfoProvider.Id);
    }
}