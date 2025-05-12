namespace Test.Exceptions;

public class NoSuchAppointmentException : Exception
{
    public NoSuchAppointmentException(string? message) : base(message)
    {
    }
}