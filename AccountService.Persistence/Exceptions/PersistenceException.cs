using System;

namespace AccountService.Persistence.Exceptions
{
    public class PersistenceException : Exception
    {
        public PersistenceException() { }

        public PersistenceException(string message) : base(message) { }

        public PersistenceException(string message, Exception ex) : base(message, ex) { }
    }
}
