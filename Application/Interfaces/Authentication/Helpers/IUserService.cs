using Application.DTO.Authentication;
using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Authentication.Helpers
{
    public interface IUserService
    {
        Task<UserInfoResponse> GetUserInfoData(string accessToken);
        Task<ApplicationUser> GetIdentityUser(UserInfoResponse userInfoData);
    }
}
