using System;

namespace BiblioClasse
{
    public class InvalidUserException : Exception
    {
        /// <summary>
        /// Classe d'exception liée aux erreurs d'un utilisateur et de son comportement
        /// </summary>
        /// <param name="message">Message d'erreur</param>
        public InvalidUserException(string message) : base(message)
        {
        }
    }
}
