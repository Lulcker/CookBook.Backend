using CookBook.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Backend.Persistence.EntityConfigs;

/// <summary>
/// Конфигурация продукта
/// </summary>
public class ProductConfig : EntityTypeConfigurationBase<Product>
{
    public override void ConfigureMore(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(p => p.Name);
        
        builder.HasIndex(p => p.Name).IsUnique();
        
        builder.HasMany(i => i.Ingredients)
            .WithOne(p => p.Product)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}