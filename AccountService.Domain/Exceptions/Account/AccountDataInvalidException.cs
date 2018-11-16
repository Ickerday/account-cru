using System;

namespace AccountService.Domain.Exceptions.Account
{
    public class AccountDataInvalidException : AccountException
    {
        public AccountDataInvalidException() { }

        public AccountDataInvalidException(string message) : base(message) { }

        public AccountDataInvalidException(string message, Exception ex) : base(message, ex) { }
    }
}
