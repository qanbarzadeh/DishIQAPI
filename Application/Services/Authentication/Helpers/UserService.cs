using Application.DTO.Authentication;
using Application.Interfaces.Authentication.Helpers;
using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Application.Services.Authentication.Helpers
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserService(UserManager<ApplicationUser> userManager, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UserInfoResponse> GetUserInfoData(string accessToken)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var userInfoResponse = await client.GetAsync("https://graph.microsoft.com/v1.0/me");

            if (!userInfoResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch user info");
            }

            var userInfoContent = await userInfoResponse.Content.ReadAsStringAsync();
            var userInfoData = JsonConvert.DeserializeObject<UserInfoResponse>(userInfoContent);

            if (userInfoData == null)
            {
                throw new Exception("Failed to deserialize user info response");
            }

            return userInfoData;
        }

        public async Task<ApplicationUser> GetIdentityUser(UserInfoResponse userInfoData)
        {
            var identityUser = await _userManager.FindByEmailAsync(userInfoData.Email);
            if (identityUser == null)
            {
                identityUser = new ApplicationUser { UserName = userInfoData.Email, Email = userInfoData.Email };
                var result = await _userManager.CreateAsync(identityUser);

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create IdentityUser");
                }
            }
            return  identityUser;
        }

        
    }
}
