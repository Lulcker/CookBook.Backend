using AutoMapper;
using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.App.Queries.Recipes.Models;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Backend.App.Queries.Recipes;

/// <summary>
/// Запрос для получения рецептов у пользователя
/// </summary>
public class GetRecipesByUserQuery(
    IRepository<Recipe> recipeRepository,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    IMapper mapper)
{
    public async Task<IReadOnlyCollection<RecipeModel>> Execute(GetAllRecipesByUserInputModel inputModel)
    {
        accessRightProvider.CheckIsAuthorized();
        
        var recipes = recipeRepository
            .Include(rc => rc.RecipeComments)
            .Include(fr => fr.FavoriteRecipes)
            .ThenInclude(u => u.User)
            .Include(pq => pq.Ingredients)
            .ThenInclude(p => p.Product)
            .Where(r => r.UserId == userInfoProvider.Id && r.RecipeStatus == inputModel.Status)
            .AsSplitQuery()
            .AsNoTracking()
            .AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(inputModel.Search))
            recipes = recipes
                .Where(c => c.Name.ToLower()
                    .Contains(inputModel.Search.ToLower()));
        
        recipes = recipes.OrderByRecipe(inputModel.OrderBy);

        recipes = recipes
            .Skip(ConstHelper.PerPage * inputModel.Page)
            .Take(ConstHelper.PerPage);

        return mapper.Map<IReadOnlyCollection<RecipeModel>>(await recipes.ToListAsync());
    }
}