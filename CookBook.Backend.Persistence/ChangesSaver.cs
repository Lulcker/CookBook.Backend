namespace CookBook.Backend.Persistence;

public class ChangesSaver : Base.ChangesSaver<CookBookDbContext>, IChangesSaver
{
    public ChangesSaver(CookBookDbContext context) : base(context)
    {
    }
}

public interface IChangesSaver : Base.IChangesSaver
{
}