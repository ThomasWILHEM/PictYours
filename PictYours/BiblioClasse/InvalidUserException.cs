using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
