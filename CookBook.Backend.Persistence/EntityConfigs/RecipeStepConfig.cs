using CookBook.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Backend.Persistence.EntityConfigs;

/// <summary>
/// Конфигурация шага рецепта
/// </summary>
public class RecipeStepConfig : EntityTypeConfigurationBase<RecipeStep>
{
    public override void ConfigureMore(EntityTypeBuilder<RecipeStep> builder)
    {
        
    }
}