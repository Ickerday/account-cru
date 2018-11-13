using System;

namespace AccountService.Application.Exceptions
{
    public class AccountException : Exception
    {
        public AccountException()
        {
        }
        public AccountException(string message) : base(message)
        {
        }
    }
}
