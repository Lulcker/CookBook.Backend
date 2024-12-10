using AutoMapper;
using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.App.Queries.Recipes.Models;
using CookBook.Backend.Domain.Dictionaries;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Backend.App.Queries.Recipes;

/// <summary>
/// Запрос для получения рецептов для администратора
/// </summary>
public class GetSendToModerationRecipesQuery(
    IRepository<Recipe> recipeRepository,
    IAccessRightProvider accessRightProvider,
    IMapper mapper)
{
    public async Task<IReadOnlyCollection<RecipeModel>> Execute(GetAllRecipesForAdministratorInputModel inputModel)
    {
        accessRightProvider.CheckIsAdministrator();

        if (inputModel.Status is RecipeStatus.Published)
            throw new BusinessException("Рецепт не должен быть опубликован");
        
        var recipes = recipeRepository
            .Include(rc => rc.RecipeComments)
            .Include(fr => fr.FavoriteRecipes)
            .ThenInclude(u => u.User)
            .Include(pq => pq.Ingredients)
            .ThenInclude(p => p.Product)
            .Where(r => r.RecipeStatus == inputModel.Status)
            .OrderBy(r => r.CreatedDateTime)
            .AsSplitQuery()
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(inputModel.Search))
            recipes = recipes
                .Where(c => c.Name.ToLower()
                    .Contains(inputModel.Search.ToLower()));

        recipes = recipes
            .Skip(ConstHelper.PerPage * inputModel.Page)
            .Take(ConstHelper.PerPage);
        
        return mapper.Map<IReadOnlyCollection<RecipeModel>>(await recipes.ToListAsync());
    }
}