using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Backend.App.Commands.Categories;

/// <summary>
/// Команда удаления категории
/// </summary>
public class DeleteCategoryCommand(
    IRepository<Category> categoryRepository,
    IChangesSaver changesSaver,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    ILogger<DeleteCategoryCommand> logger)
{
    public async Task Execute(Guid categoryId)
    {
        accessRightProvider.CheckIsAdministrator();
        
        var category = await categoryRepository
            .Include(c => c.Recipes)
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category is null)
            throw new BusinessException("Категория не найдена");

        if (category.Recipes.Count > 0)
            throw new BusinessException("Нельзя удалит категорию, так как к ней привязаны рецепты");
        
        var categoryName = category.Name;

        await categoryRepository.RemoveAsync(categoryId);
        await changesSaver.SaveChangesAsync();
        
        logger.LogInformation("Удалена категория {CategoryName} администратором {AdministratorName}  с Id: {AdministratorId}",
            categoryName, userInfoProvider.Login, userInfoProvider.Id);
    }
}