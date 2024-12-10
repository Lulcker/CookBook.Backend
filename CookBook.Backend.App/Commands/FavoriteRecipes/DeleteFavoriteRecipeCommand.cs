using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Backend.App.Commands.FavoriteRecipes;

/// <summary>
/// Команда удаления рецепта из избранного
/// </summary>
public class DeleteFavoriteRecipeCommand(
    IRepository<FavoriteRecipe> favoriteRecipeRepository,
    IChangesSaver changesSaver,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    ILogger<DeleteFavoriteRecipeCommand> logger
    )
{
    public async Task Execute(Guid recipeId)
    {
        accessRightProvider.CheckIsCustomer();

        var favoriteRecipe = await favoriteRecipeRepository
            .FirstOrDefaultAsync(fr => fr.RecipeId == recipeId && fr.UserId == userInfoProvider.Id);

        if (favoriteRecipe is null)
            throw new BusinessException("Рецепт не найден");

        await favoriteRecipeRepository.RemoveAsync(favoriteRecipe.Id);
        await changesSaver.SaveChangesAsync();
        
        logger.LogInformation("Удалён рецепт с Id: {RecipeId} из избранного у пользователя {Login} с Id: {UserId}",
             recipeId, userInfoProvider.Login, userInfoProvider.Id);
    }
}