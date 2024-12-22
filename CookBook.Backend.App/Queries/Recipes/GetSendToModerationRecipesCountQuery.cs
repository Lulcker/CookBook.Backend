using CookBook.Backend.App.Contracts;
using CookBook.Backend.Domain.Dictionaries;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Backend.App.Queries.Recipes;

/// <summary>
/// Запрос получения количества рецептов для модерации
/// </summary>
public class GetSendToModerationRecipesCountQuery(
    IRepository<Recipe> recipeRepository,
    IAccessRightProvider accessRightProvider
    )
{
    public async Task<int> Execute()
    {
        accessRightProvider.CheckIsAdministrator();

        return await recipeRepository
            .AsNoTracking()
            .CountAsync(r => r.RecipeStatus == RecipeStatus.SendToModeration);
    }
}