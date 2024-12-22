using AutoMapper;
using CookBook.Backend.App.Contracts;
using CookBook.Backend.App.Queries.Categories.Models;
using CookBook.Backend.App.Queries.FavoriteRecipes.Models;
using CookBook.Backend.App.Queries.Products.Models;
using CookBook.Backend.App.Queries.Recipes.Models;
using CookBook.Backend.Domain.Dictionaries;
using CookBook.Backend.Domain.Entities;

namespace CookBook.Backend.App.Mappings;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CategoryMaps();
        ProductMaps();
        RecipeMaps();
        FavoriteRecipeMaps();
    }

    private void CategoryMaps()
    {
        CreateMap<Category, CategoryModel>();
    }

    private void ProductMaps()
    {
        CreateMap<Product, ProductModel>();
    }

    private void RecipeMaps()
    {
        CreateMap<Recipe, RecipeModel>()
            .ForMember(r => r.Rating, 
                t => t.MapFrom(entity => 
                    entity.RecipeComments.Any() ? entity.RecipeComments.Average(r => r.Rating) : 0))
            .ForMember(r => r.IsAddedToFavorite,
                t => t.MapFrom<IsAddedToFavoriteRecipeResolver>())
            .ForMember(r => r.Ingredients,
                t => t.MapFrom(entity =>
                    string.Join(", ", entity.Ingredients.Select(pq => pq.Product.Name))));
            
        CreateMap<Recipe, RecipeFullInfoModel>()
            .ForMember(r => r.CategoryName, t => t.MapFrom(entity => entity.Category.Name))
            .ForMember(r => r.UserLogin, t => t.MapFrom(entity => entity.User.Login))
            .ForMember(r => r.Rating, t => t.MapFrom(entity => entity.RecipeComments.Any() ? entity.RecipeComments.Average(r => r.Rating) : 0))
            .ForMember(r => r.Ingredients, opt => opt.MapFrom(entity => entity.Ingredients.Select(pq => new IngredientModel
            {
                Id = pq.Id,
                ProductName = pq.Product.Name,
                Quantity = pq.Quantity,
                UnitOfMeasure = pq.UnitOfMeasure
            })))
            .ForMember(r => r.RecipeSteps, opt => opt.MapFrom(entity => entity.RecipeSteps.Select(rs => new RecipeStepModel
            {
                Id = rs.Id,
                Description = rs.Description,
                PhotoLink = rs.PhotoLink
            })))
            .ForMember(r => r.IsAddedToFavorite,
                t => t.MapFrom<IsAddedToFavoriteRecipeFullInfoResolver>()); 
    }

    private void FavoriteRecipeMaps()
    {
        CreateMap<FavoriteRecipe, FavoriteRecipeModel>()
            .ForMember(fr => fr.RecipeId, opt => opt.MapFrom(entity => entity.Recipe.Id))
            .ForMember(fr => fr.Name, opt => opt.MapFrom(entity => entity.Recipe.Name))
            .ForMember(fr => fr.PhotoLink, opt => opt.MapFrom(entity => entity.Recipe.PhotoLink))
            .ForMember(fr => fr.Ingredients, opt => opt.MapFrom(entity => string.Join(", ", entity.Recipe.Ingredients.Select(i => i.Product.Name))));
    }
}

public class IsAddedToFavoriteRecipeResolver(IUserInfoProvider userInfoProvider) : IValueResolver<Recipe, RecipeModel, bool>
{
    public bool Resolve(Recipe source, RecipeModel destination, bool destMember, ResolutionContext context)
    {
        return source.FavoriteRecipes.Any(u => u.UserId == userInfoProvider.Id);
    }
}

public class IsAddedToFavoriteRecipeFullInfoResolver(IUserInfoProvider userInfoProvider) : IValueResolver<Recipe, RecipeFullInfoModel, bool>
{
    public bool Resolve(Recipe source, RecipeFullInfoModel destination, bool destMember, ResolutionContext context)
    {
        return source.FavoriteRecipes.Any(u => u.UserId == userInfoProvider.Id);
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member