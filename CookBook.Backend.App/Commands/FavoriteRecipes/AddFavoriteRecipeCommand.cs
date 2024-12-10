using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Backend.App.Commands.FavoriteRecipes;

/// <summary>
/// Команда добавления рецепта в избранное
/// </summary>
public class AddFavoriteRecipeCommand(
    IRepository<Recipe> recipeRepository,
    IRepository<FavoriteRecipe> favoriteRecipeRepository,
    IChangesSaver changesSaver,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    ILogger<AddFavoriteRecipeCommand> logger)
{
    public async Task Execute(Guid recipeId)
    {
        accessRightProvider.CheckIsCustomer();

        var recipe = await recipeRepository
            .FirstOrDefaultAsync(r => r.Id == recipeId);

        if (recipe is null)
            throw new BusinessException("Рецепт не найден");

        var favoriteRecipe = await favoriteRecipeRepository
            .FirstOrDefaultAsync(fr => fr.UserId == userInfoProvider.Id && fr.RecipeId == recipeId);

        if (favoriteRecipe is not null)
            throw new BusinessException("Рецепт уже есть в избранном");

        favoriteRecipe = new FavoriteRecipe
        {
            RecipeId = recipeId,
            UserId = userInfoProvider.Id!.Value,
            AddedDateTime = DateTime.UtcNow
        };

        await favoriteRecipeRepository.AddAsync(favoriteRecipe);
        await changesSaver.SaveChangesAsync();
        
        logger.LogInformation("Добавлен рецепт {RecipeName} с Id: {RecipeId} в избранное пользователю {Login} с Id: {UserId}",
            recipe.Name, recipe.Id, userInfoProvider.Login, userInfoProvider.Id);
    }
}