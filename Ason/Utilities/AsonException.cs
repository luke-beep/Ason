namespace Ason.Ason.Utilities;

public class AsonException : Exception
{
    public AsonException()
    {
    }

    public AsonException(string message) : base(message)
    {
    }

    public AsonException(string message, Exception inner) : base(message, inner)
    {
    }
}