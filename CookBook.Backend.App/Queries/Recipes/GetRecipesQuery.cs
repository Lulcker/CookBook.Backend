using AutoMapper;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.App.Queries.Recipes.Models;
using CookBook.Backend.Domain.Dictionaries;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Backend.App.Queries.Recipes;

/// <summary>
/// Запрос для получения рецептов
/// </summary>
public class GetRecipesQuery(
    IRepository<Recipe> recipeRepository,
    IMapper mapper)
{
    public async Task<IReadOnlyCollection<RecipeModel>> Execute(GetRecipesInputModel inputModel)
    {
        var recipes = recipeRepository
            .Include(rc => rc.RecipeComments)
            .Include(fr => fr.FavoriteRecipes)
            .ThenInclude(u => u.User)
            .Include(pq => pq.Ingredients)
            .ThenInclude(p => p.Product)
            .Where(r => r.RecipeStatus == RecipeStatus.Published)
            .AsSplitQuery()
            .AsNoTracking()
            .AsQueryable();

        if (inputModel.CategoryId.HasValue)
            recipes = recipes.Where(r => r.CategoryId == inputModel.CategoryId.Value);

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