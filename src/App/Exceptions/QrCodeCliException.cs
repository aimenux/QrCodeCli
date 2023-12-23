namespace App.Exceptions;

public class QrCodeCliException : Exception
{
    public QrCodeCliException(string message) : base(message)
    {
    }
}