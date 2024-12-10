using CookBook.Backend.Domain.Entities;

namespace CookBook.Backend.Persistence;

public class Repository<TEntity> : Base.Repository<TEntity>, IRepository<TEntity> where TEntity : EntityBase
{
    public Repository(CookBookDbContext context) : base(context)
    {
    }
}

public interface IRepository<TEntity> : Base.IRepository<TEntity> where TEntity : EntityBase
{
}