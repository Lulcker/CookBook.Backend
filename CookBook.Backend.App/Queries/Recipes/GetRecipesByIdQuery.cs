using AutoMapper;
using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Exceptions;
using CookBook.Backend.App.Queries.Recipes.Models;
using CookBook.Backend.Domain.Dictionaries;
using CookBook.Backend.Domain.Entities;
using CookBook.Backend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Backend.App.Queries.Recipes;

/// <summary>
/// Запрос для получения рецепта по Id
/// </summary>
public class GetRecipesByIdQuery(
    IRepository<Recipe> recipeRepository,
    IAccessRightProvider accessRightProvider,
    IUserInfoProvider userInfoProvider,
    IMapper mapper)
{
    public async Task<RecipeFullInfoModel> Execute(Guid recipeId)
    {
        var recipe = await recipeRepository
            .Include(u => u.User)
            .Include(c => c.Category)
            .Include(rc => rc.RecipeComments)
            .Include(rs => rs.RecipeSteps)
            .Include(pq => pq.Ingredients)
            .ThenInclude(p => p.Product)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(r => r.Id == recipeId);

        if (recipe is null)
            throw new BusinessException("Рецепт не найден");
        
        if (recipe.RecipeStatus != RecipeStatus.Published && recipe.UserId != userInfoProvider.Id)
            accessRightProvider.CheckIsAdministrator();

        return mapper.Map<RecipeFullInfoModel>(recipe);
    }
}