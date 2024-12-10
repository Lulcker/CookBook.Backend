using AutoMapper;
using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.App.Queries.FavoriteRecipes.Models;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Backend.App.Queries.FavoriteRecipes;

/// <summary>
/// Запрос для получения рецептов в избранном
/// </summary>
public class GetFavoriteRecipesQuery(
    IRepository<FavoriteRecipe> favoriteRecipeRepository,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    IMapper mapper
    )
{
    public async Task<IReadOnlyCollection<FavoriteRecipeModel>> Execute(GetFavoriteRecipesInputModel inputModel)
    {
        accessRightProvider.CheckIsCustomer();

        var recipes = favoriteRecipeRepository
            .Where(fr => fr.UserId == userInfoProvider.Id)
            .AsNoTracking()
            .AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(inputModel.Search))
            recipes = recipes
                .Where(c => c.Recipe.Name.ToLower()
                    .Contains(inputModel.Search.ToLower()));
        
        recipes = recipes.OrderByFavoriteRecipe(inputModel.OrderBy);

        recipes = recipes
            .Skip(ConstHelper.PerPage * inputModel.Page)
            .Take(ConstHelper.PerPage);

        return await mapper
            .ProjectTo<FavoriteRecipeModel>(recipes)
            .ToListAsync();
    }
}