namespace AccountService.Application.Exceptions.Specification
{
    public class SpecificationException : Exception
    {
        public SpecificationException() { }

        public SpecificationException(string message) : base(message) { }

        public SpecificationException(string message, Exception ex) : base(message, ex) { }
    }
}
