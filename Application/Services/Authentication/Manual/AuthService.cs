using Application.Interfaces.Authentication.Helpers;
using Application.Interfaces.Authentication.Manual;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.UserEntities; // <-- You might need to adjust this according to your actual namespace

namespace Application.Services.Authentication.Manual
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<IdentityResult> RegisterAsync(string email, string password)
        {
            var user = new ApplicationUser { UserName = email, Email = email };
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                // Generate token
                return await _tokenService.GenerateToken(user);
            }

            throw new Exception("Invalid login attempt.");
        }

        
    }
}
