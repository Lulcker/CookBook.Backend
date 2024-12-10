using CookBook.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Backend.Persistence.EntityConfigs;

/// <summary>
/// Конфигурация избранных рецептов
/// </summary>
public class FavoriteRecipeConfig : EntityTypeConfigurationBase<FavoriteRecipe>
{
    public override void ConfigureMore(EntityTypeBuilder<FavoriteRecipe> builder)
    {
        builder.HasIndex(fr => fr.UserId);
        
        builder.HasIndex(fr => new { fr.UserId, fr.RecipeId}).IsUnique();
    }
}