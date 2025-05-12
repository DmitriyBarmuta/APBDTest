namespace Test.Exceptions;

public class NoSuchServiceException : Exception
{
    public NoSuchServiceException(string? message) : base(message)
    {
        
    }
}