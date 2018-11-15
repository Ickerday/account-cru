using System;

namespace AccountService.Core.Exceptions.Specification
{
    public class InvalidSpecificationException : SpecificationException
    {
        public InvalidSpecificationException() { }

        public InvalidSpecificationException(string message) : base(message) { }

        public InvalidSpecificationException(string message, Exception ex) : base(message, ex) { }
    }
}
