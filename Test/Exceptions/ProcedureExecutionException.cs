namespace Test.Exceptions;

public class ProcedureExecutionException : Exception
{
    public ProcedureExecutionException(string? message) : base(message)
    {
    }
}