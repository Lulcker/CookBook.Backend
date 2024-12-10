using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CookBook.Backend.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CookBookDbContext>
{
    public CookBookDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<CookBookDbContext>();
        
        builder.UseNpgsql("Host=localhost;Port=5432;Database=cookbook_db;User ID=postgres;Password=password_pg;CommandTimeout=60", options => { });

        return new CookBookDbContext(builder.Options);
    }
}