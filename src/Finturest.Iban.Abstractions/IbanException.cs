namespace Finturest.Iban.Abstractions;

public class IbanException : Exception
{
    public IbanException()
    { }

    public IbanException(string message) : base(message)
    { }

    public IbanException(string message, Exception? innerException) : base(message, innerException)
    { }
}
