using CookBook.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Backend.Persistence.EntityConfigs;

/// <summary>
/// Конфигурация категории
/// </summary>
public class CategoryConfig : EntityTypeConfigurationBase<Category>
{
    public override void ConfigureMore(EntityTypeBuilder<Category> builder)
    {
        builder.HasIndex(c => c.Name);
        
        builder.HasMany(r => r.Recipes)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}