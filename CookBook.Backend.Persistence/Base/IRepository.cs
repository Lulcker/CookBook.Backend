using System.Collections;
using System.Linq.Expressions;
using CookBook.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Backend.Persistence.Base;

public interface IRepository<TEntity> :
    IQueryable<TEntity>,
    IEnumerable<TEntity>,
    IEnumerable,
    IQueryable,
    IAsyncEnumerable<TEntity>
    where TEntity : class
{
    Task AddAsync(TEntity objectToAdd);

    Task AddRangeAsync(IEnumerable<TEntity> objectsToAdd);

    Task RemoveAsync(Guid id);

    Task RemoveRangeAsync(IEnumerable<Guid> ids);
}

public class Repository<TEntity> : 
    IRepository<TEntity>
    where TEntity : EntityBase
{
    private readonly CookBookDbContext context;
    private readonly IQueryable<TEntity> sourceQuery;
    
    public Repository(CookBookDbContext context)
    {
        this.context = context;
        this.sourceQuery = context.Set<TEntity>();
    }
    
    public async Task AddAsync(TEntity objectToAdd)
    {
        await context.Set<TEntity>().AddAsync(objectToAdd);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> objectsToAdd)
    {
        await context.Set<TEntity>().AddRangeAsync(objectsToAdd);
    }

    public async Task RemoveAsync(Guid id)
    {
        TEntity? entity = await context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        
        if (entity is null)
            return;
        
        context.Set<TEntity>().Remove(entity);
    }

    public async Task RemoveRangeAsync(IEnumerable<Guid> ids)
    {
        List<TEntity> entities = await context.Set<TEntity>().Where(e => ids.Contains(e.Id)).ToListAsync();
        
        context.Set<TEntity>().RemoveRange(entities);
    }

    public IEnumerator<TEntity> GetEnumerator() => sourceQuery.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public Type ElementType => sourceQuery.ElementType;

    public Expression Expression => sourceQuery.Expression;

    public IQueryProvider Provider => sourceQuery.Provider;
    
    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return ((IAsyncEnumerable<TEntity>)sourceQuery).GetAsyncEnumerator(cancellationToken);
    }
}