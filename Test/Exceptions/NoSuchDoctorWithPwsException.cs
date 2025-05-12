namespace Test.Exceptions;

public class NoSuchDoctorWithPwsException : Exception
{
    public NoSuchDoctorWithPwsException(string? message) : base(message)
    {
    }
}