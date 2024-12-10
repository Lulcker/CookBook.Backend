using CookBook.Backend.App.Commands.Recipes.Models;
using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.Domain.Dictionaries;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Backend.App.Commands.Recipes;

/// <summary>
/// Команда отклонения рецепта
/// </summary>
public class RejectRecipeCommand(
    IRepository<Recipe> recipeRepository,
    IChangesSaver changesSaver,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    ILogger<RejectRecipeCommand> logger)
{
    public async Task Execute(RejectRecipeInputModel inputModel)
    {
        accessRightProvider.CheckIsAdministrator();

        var recipe = await recipeRepository
            .FirstOrDefaultAsync(r => r.Id == inputModel.Id);

        if (recipe is null)
            throw new BusinessException("Рецепт не найден");

        if (recipe.RecipeStatus != RecipeStatus.SendToModeration)
            throw new BusinessException("Рецепт находится не в статусе 'Отправлен на модерацию'");

        recipe.RecipeStatus = RecipeStatus.Reject;
        recipe.RejectMessage = inputModel.Comment;

        await changesSaver.SaveChangesAsync();
        
        logger.LogInformation("Отклонён рецепт {RecipeName} с Id: {RecipeId} администратором {AdministratorName} с Id: {AdministratorId}",
            recipe.Name, inputModel.Id, userInfoProvider.Login, userInfoProvider.Id);
    }
}