using System;

namespace BiblioClasse
{
    public class InvalidPhotoException : Exception
    {
        public InvalidPhotoException(string message) : base(message)
        {
        }

        public InvalidPhotoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
