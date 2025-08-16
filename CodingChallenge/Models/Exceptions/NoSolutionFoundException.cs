namespace CodingChallenge.Models.Exceptions;

public class NoSolutionFoundException : Exception
{
    public NoSolutionFoundException(string message) : base(message) { }
    public NoSolutionFoundException(string message, Exception innerException) : base(message, innerException) { }
}