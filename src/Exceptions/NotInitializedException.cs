namespace LiteConfiguration.Exceptions;

public class NotInitializedException : Exception
{
    internal NotInitializedException ()
    {

    }

    internal NotInitializedException (string message) : base(message)
    {

    }

    internal NotInitializedException (string message, Exception inner) : base(message, inner)
    {

    }
}