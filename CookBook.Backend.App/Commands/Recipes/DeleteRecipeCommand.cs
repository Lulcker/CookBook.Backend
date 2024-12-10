using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Backend.App.Commands.Recipes;

/// <summary>
/// Команда удаления рецепта
/// </summary>
public class DeleteRecipeCommand(
    IRepository<Recipe> recipeRepository,
    IRepository<RecipeStep> recipeStepRepository,
    IRepository<RecipeComment> recipeCommentRepository,
    IRepository<Ingredient> productQuantityRepository,
    IChangesSaver changesSaver,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    ILogger<DeleteRecipeCommand> logger)
{
    public async Task Execute(Guid recipeId)
    {
        accessRightProvider.CheckIsAdministrator();
        
        var recipe = await recipeRepository
            .FirstOrDefaultAsync(r => r.Id == recipeId);

        if (recipe is null)
            throw new BusinessException("Рецепт не найден");
        
        var recipeStepIds = await recipeStepRepository
            .Where(r => r.RecipeId == recipeId)
            .Select(pq => pq.Id)
            .ToListAsync();

        await recipeStepRepository.RemoveRangeAsync(recipeStepIds);
        
        var recipeCommentIds = await recipeCommentRepository
            .Where(r => r.RecipeId == recipeId)
            .Select(pq => pq.Id)
            .ToListAsync();

        await recipeCommentRepository.RemoveRangeAsync(recipeCommentIds);

        var productQuantityIds = await productQuantityRepository
            .Where(r => r.RecipeId == recipeId)
            .Select(pq => pq.Id)
            .ToListAsync();

        await productQuantityRepository.RemoveRangeAsync(productQuantityIds);

        await recipeRepository.RemoveAsync(recipeId);
        await changesSaver.SaveChangesAsync();
        
        logger.LogInformation("Удалён рецепт с Id: {RecipeId} администратором {AdministratorName} с Id: {AdministratorId}",
            recipeId, userInfoProvider.Login, userInfoProvider.Id);
    }
}