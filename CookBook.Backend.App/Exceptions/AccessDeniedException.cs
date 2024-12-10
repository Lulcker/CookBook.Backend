namespace CookBook.Backend.App.Exceptions;

public class AccessDeniedException : Exception
{
    public AccessDeniedException(string message)
        : base(message) { }
}