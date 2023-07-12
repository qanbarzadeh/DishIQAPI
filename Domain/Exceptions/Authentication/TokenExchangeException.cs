namespace Domain.Exceptions.Authentication
{
    public class TokenExchangeException : Exception
    {
        public TokenExchangeException() { }
        public TokenExchangeException(string message) : base(message) { }
        public TokenExchangeException(string message, Exception inner) : base(message, inner) { }
    }

}
