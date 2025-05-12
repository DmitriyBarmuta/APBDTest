namespace Test.Exceptions;

public class InvalidPatientIdException : Exception
{
    public InvalidPatientIdException(string? message) : base(message)
    {
    }
}