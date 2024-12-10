using CookBook.Backend.App.Contracts;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Backend.App.Queries.FavoriteRecipes;

/// <summary>
/// Запрос получения количества избранных рецептов для пользователя
/// </summary>
public class GetFavoriteRecipesCountQuery(
    IRepository<FavoriteRecipe> favoriteRecipeRepository,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider
    )
{
    public async Task<int> Execute()
    {
        accessRightProvider.CheckIsCustomer();

        return await favoriteRecipeRepository
            .Where(fr => fr.UserId == userInfoProvider.Id)
            .AsNoTracking()
            .CountAsync();
    }
}