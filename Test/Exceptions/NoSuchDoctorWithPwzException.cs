namespace Test.Exceptions;

public class NoSuchDoctorWithPwzException : Exception
{
    public NoSuchDoctorWithPwzException(string? message) : base(message)
    {
    }
}