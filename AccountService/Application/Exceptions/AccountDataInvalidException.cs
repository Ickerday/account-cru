namespace AccountService.Application.Exceptions
{
    public class AccountDataInvalidException : AccountException
    {
        public AccountDataInvalidException()
        {
        }
        public AccountDataInvalidException(string message) : base(message)
        {
        }
    }
}
