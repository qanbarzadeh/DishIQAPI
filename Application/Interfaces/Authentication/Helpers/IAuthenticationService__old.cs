using Application.DTO.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Authentication.Helpers
{
    public interface IAuthenticationService__old
    {
        Task<string> InitiateExternalAuthenticationAsync(string provider);
        Task<AuthenticationResult> HandleExternalAuthenticationCallbackAsync(string provider, string authorizationCode);

    }
}
