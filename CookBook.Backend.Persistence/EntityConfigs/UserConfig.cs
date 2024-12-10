using CookBook.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Backend.Persistence.EntityConfigs;

/// <summary>
/// Конфигурация пользователя
/// </summary>
public class UserConfig : EntityTypeConfigurationBase<User>
{
    public override void ConfigureMore(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Login);

        builder.HasIndex(u => u.Login).IsUnique();
        
        builder.HasMany(r => r.Recipes)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(fr => fr.FavoriteRecipes)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}