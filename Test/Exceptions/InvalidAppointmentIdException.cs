namespace Test.Exceptions;

public class InvalidAppointmentIdException : Exception
{
    public InvalidAppointmentIdException(string? message) : base(message)
    {
    }
}