using System;

namespace DAL.Exceptions
{
    public class InvalidNextTokenException : Exception
    {
        public InvalidNextTokenException()
        {
        }

        public InvalidNextTokenException(string message) : base(message)
        {
        }

        public InvalidNextTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
