using System;

namespace AccountService.Application.Exceptions.Account
{
    public class AccountNotFoundException : AccountException
    {
        public AccountNotFoundException() { }

        public AccountNotFoundException(string message) : base(message) { }

        public AccountNotFoundException(string message, Exception ex) : base(message, ex) { }
    }
}
