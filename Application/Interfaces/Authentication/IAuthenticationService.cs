using Application.DTO.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> InitiateExternalAuthenticationAsync(string provider, string redirectUri);
        Task<AuthenticationResult> HandleExternalAuthenticationCallbackAsync(string provider, string authorizationCode);
    }
}
