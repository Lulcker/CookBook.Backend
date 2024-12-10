using CookBook.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Backend.Persistence.EntityConfigs;

public class RecipeCommentConfig : EntityTypeConfigurationBase<RecipeComment>
{
    public override void ConfigureMore(EntityTypeBuilder<RecipeComment> builder)
    {
        
    }
}