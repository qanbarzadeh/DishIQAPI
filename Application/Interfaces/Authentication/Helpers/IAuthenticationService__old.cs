using Application.DTO.Authentication;

namespace Application.Interfaces.Authentication.Helpers
{
    public interface IAuthenticationService__old
    {
        Task<string> InitiateExternalAuthenticationAsync(string provider);
        Task<AuthenticationResult> HandleExternalAuthenticationCallbackAsync(string provider, string authorizationCode);

    }
}
