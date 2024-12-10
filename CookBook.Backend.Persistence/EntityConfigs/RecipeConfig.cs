using CookBook.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Backend.Persistence.EntityConfigs;

/// <summary>
/// Конфигурация рецепта
/// </summary>
public class RecipeConfig : EntityTypeConfigurationBase<Recipe>
{
    public override void ConfigureMore(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasIndex(r => r.Name);
        
        builder.HasMany(rs => rs.RecipeSteps)
            .WithOne(r => r.Recipe)
            .HasForeignKey(r => r.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(i => i.Ingredients)
            .WithOne(r => r.Recipe)
            .HasForeignKey(r => r.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(fr => fr.FavoriteRecipes)
            .WithOne(r => r.Recipe)
            .HasForeignKey(r => r.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(rc => rc.RecipeComments)
            .WithOne(r => r.Recipe)
            .HasForeignKey(r => r.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}