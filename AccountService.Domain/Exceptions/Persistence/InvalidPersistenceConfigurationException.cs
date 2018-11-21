using System;

namespace AccountService.Domain.Exceptions.Persistence
{
    public class InvalidPersistenceConfigurationException : PersistenceException
    {
        public InvalidPersistenceConfigurationException() { }

        public InvalidPersistenceConfigurationException(string message) : base(message) { }

        public InvalidPersistenceConfigurationException(string message, Exception ex) : base(message, ex) { }
    }
}
