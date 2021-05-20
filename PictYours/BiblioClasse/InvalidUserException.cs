using System;

namespace BiblioClasse
{
    public class InvalidUserException : Exception
    {
        public InvalidUserException(string message) : base(message)
        {
        }

        public InvalidUserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
