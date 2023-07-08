using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.Authentication
{
    public class TokenExchangeException : Exception
    {
        public TokenExchangeException() { }
        public TokenExchangeException(string message) : base(message) { }
        public TokenExchangeException(string message, Exception inner) : base(message, inner) { }
    }

}
