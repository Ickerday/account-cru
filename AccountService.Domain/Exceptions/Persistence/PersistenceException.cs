using System;

namespace AccountService.Domain.Exceptions.Persistence
{
    public class PersistenceException : Exception
    {
        public PersistenceException() { }

        public PersistenceException(string message) : base(message) { }

        public PersistenceException(string message, Exception ex) : base(message, ex) { }
    }
}
