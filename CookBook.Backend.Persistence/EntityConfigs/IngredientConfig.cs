using CookBook.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Backend.Persistence.EntityConfigs;

/// <summary>
/// Конфигурация количества продукта
/// </summary>
public class IngredientConfig : EntityTypeConfigurationBase<Ingredient>
{
    public override void ConfigureMore(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasIndex(pq => pq.RecipeId);
        
        builder.HasIndex(pq => new { pq.ProductId, pq.RecipeId}).IsUnique();
    }
}