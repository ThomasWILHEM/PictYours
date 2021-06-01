using System;

namespace BiblioClasse
{
    public class InvalidPhotoException : Exception
    {

        /// <summary>
        /// Classe d'exception liée aux erreurs d'une photo
        /// </summary>
        /// <param name="message">Message d'erreur</param>
        public InvalidPhotoException(string message) : base(message)
        {
        }
    }
}
