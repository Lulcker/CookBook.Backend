using Microsoft.EntityFrameworkCore;

namespace CookBook.Backend.Persistence.Base;

public interface IChangesSaver
{
    Task SaveChangesAsync();
}

public class ChangesSaver<TDbContext> : IChangesSaver where TDbContext : DbContext
{
    private readonly TDbContext context;

    public ChangesSaver(TDbContext context)
    {
        this.context = context;
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}