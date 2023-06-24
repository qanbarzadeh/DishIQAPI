using Application.DTO.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Authentication.Helpers
{
    public interface ITokenService
    {
        Task<TokenResponse> GetTokenResponseData(string authorizationCode);
    }

}
