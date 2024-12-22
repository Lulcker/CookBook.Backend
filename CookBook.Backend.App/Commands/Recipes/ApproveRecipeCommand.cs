using CookBook.Backend.App.Commands.Recipes.Models;
using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.Domain.Dictionaries;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Backend.App.Commands.Recipes;

/// <summary>
/// Команда одобрения рецепта
/// </summary>
public class ApproveRecipeCommand(
    IRepository<Recipe> recipeRepository,
    IChangesSaver changesSaver,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    ILogger<ApproveRecipeCommand> logger)
{
    public async Task Execute(ApproveRecipeInputModel inputModel)
    {
        accessRightProvider.CheckIsAdministrator();

        var recipe = await recipeRepository
            .Include(rs => rs.RecipeSteps)
            .FirstOrDefaultAsync(r => r.Id == inputModel.Id);

        if (recipe is null)
            throw new BusinessException("Рецепт не найден");

        if (recipe.RecipeStatus != RecipeStatus.SendToModeration)
            throw new BusinessException("Рецепт находится не в статусе 'Отправлен на модерацию'");
        
        InputModelHelper.TrimStringProperties(inputModel);

        recipe.RecipeStatus = RecipeStatus.Published;
        recipe.Name = inputModel.Name;
        recipe.Description = inputModel.Description;

        await changesSaver.SaveChangesAsync();
        
        logger.LogInformation("Одобрен рецепт {RecipeName} с Id: {RecipeId} администратором {AdministratorName} с Id: {AdministratorId}",
            recipe.Name, inputModel.Id, userInfoProvider.Login, userInfoProvider.Id);
    }
}