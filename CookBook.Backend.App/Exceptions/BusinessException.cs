namespace CookBook.Backend.App.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message)
        : base(message) { }
}