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
/// Команда обновления категории
/// </summary>
public class UpdateCategoryCommand(
    IRepository<Category> categoryRepository,
    IChangesSaver changesSaver,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    ILogger<UpdateCategoryCommand> logger)
{
    public async Task Execute(UpdateCategoryInputModel inputModel)
    {
        accessRightProvider.CheckIsAdministrator();
        
        InputModelHelper.TrimStringProperties(inputModel);
        
        if (string.IsNullOrWhiteSpace(inputModel.Name))
            throw new BusinessException("Название категории не может быть пустым");
        
        if (string.IsNullOrWhiteSpace(inputModel.Description))
            throw new BusinessException("Описание категории не может быть пустым");
        
        var category = await categoryRepository
            .FirstOrDefaultAsync(c => c.Name.ToLower() == inputModel.Name.ToLower());

        if (category is not null && category.Id != inputModel.Id)
            throw new BusinessException("Категория с таким названием уже существует");
        
        category ??= await categoryRepository
            .Include(c => c.Recipes)
            .FirstOrDefaultAsync(c => c.Id == inputModel.Id);

        if (category is null)
            throw new BusinessException("Категория не найдена");

        category.Name = inputModel.Name;
        category.Description = inputModel.Description;

        await changesSaver.SaveChangesAsync();

        logger.LogInformation("Обновлена категория с Id: {CategoryId} администратором {AdministratorName}  с Id: {AdministratorId}",
            category.Id, userInfoProvider.Login, userInfoProvider.Id);
    }
}