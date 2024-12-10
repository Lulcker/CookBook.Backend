using CookBook.Backend.Persistence.EntityConfigs;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Backend.Persistence;

public class CookBookDbContext : DbContext
{
    public CookBookDbContext(DbContextOptions<CookBookDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfig).Assembly);
    }
}