using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Authentication.Helpers
{
    public interface ITokenService
    {
        Task<string> GenerateToken(IdentityUser user);
    }
}
