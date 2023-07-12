using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Authentication.Manual
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(string email, string password);
        Task<string> LoginAsync(string email, string password);
    }

}
