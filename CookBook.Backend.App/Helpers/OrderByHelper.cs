using CookBook.Backend.App.Exceptions;
using CookBook.Backend.App.Queries.Categories.Models;
using CookBook.Backend.App.Queries.FavoriteRecipes.Models;
using CookBook.Backend.App.Queries.Products.Models;
using CookBook.Backend.Domain.Entities;

namespace CookBook.Backend.App.Helpers;

public static class OrderByHelper
{
    private const string OrderByNameAsc = "name_asc";
    private const string OrderByNameDesc = "name_desc";
    private const string OrderByRatingAsc = "rating_asc";
    private const string OrderByRatingDesc = "rating_desc";
    private const string OrderByCreatedDateTimeAsc = "created_at_asc";
    private const string OrderByCreatedDateTimeDesc = "created_at_desc";
    private const string OrderByAddedDateTimeAsc = "added_at_asc";
    private const string OrderByAddedDateTimeDesc = "added_at_desc";
    private const string OrderByDefault = null!;
    
    public static IOrderedQueryable<Recipe> OrderByRecipe(this IQueryable<Recipe> source, string? orderBy)
    {
        return orderBy switch
        {
            OrderByNameAsc =>  source.OrderBy(r => r.Name),
            OrderByNameDesc => source.OrderByDescending(r => r.Name),
            OrderByRatingAsc => source.OrderBy(r => r.RecipeComments.Average(rc => rc.Rating)),
            OrderByRatingDesc => source.OrderByDescending(r => r.RecipeComments.Average(rc => rc.Rating)),
            OrderByCreatedDateTimeAsc => source.OrderBy(r => r.CreatedDateTime),
            OrderByCreatedDateTimeDesc => source.OrderByDescending(r => r.CreatedDateTime),
            OrderByDefault => source.OrderBy(r => r.Name),
            _ => throw new BusinessException("Такой сортировки не существует")
        };
    }
    
    public static IOrderedQueryable<Product> OrderByProduct(this IQueryable<Product> source, string? orderBy)
    {
        return orderBy switch
        {
            OrderByNameAsc => source.OrderBy(p => p.Name),
            OrderByNameDesc => source.OrderByDescending(p => p.Name),
            OrderByDefault => source.OrderBy(p => p.Name),
            _ => throw new BusinessException("Такой сортировки не существует")
        };
    }
    
    public static IOrderedQueryable<Category> OrderByCategory(this IQueryable<Category> source, string? orderBy)
    {
        return orderBy switch
        {
            OrderByNameAsc => source.OrderBy(c => c.Name),
            OrderByNameDesc => source.OrderByDescending(c => c.Name),
            OrderByDefault => source.OrderBy(c => c.Name),
            _ => throw new BusinessException("Такой сортировки не существует")
        };
    }
    
    public static IOrderedQueryable<FavoriteRecipe> OrderByFavoriteRecipe(this IQueryable<FavoriteRecipe> source, string? orderBy)
    {
        return orderBy switch
        {
            OrderByNameAsc => source.OrderBy(fr => fr.Recipe.Name),
            OrderByNameDesc => source.OrderByDescending(fr => fr.Recipe.Name),
            OrderByAddedDateTimeAsc => source.OrderBy(fr => fr.AddedDateTime),
            OrderByAddedDateTimeDesc => source.OrderByDescending(fr => fr.AddedDateTime),
            OrderByDefault => source.OrderBy(fr => fr.Recipe.Name),
            _ => throw new BusinessException("Такой сортировки не существует")
        };
    }
}