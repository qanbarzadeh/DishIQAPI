using Application.DTO.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Authentication.Helpers
{
    public interface IUserService
    {
        Task<UserInfoResponse> GetUserInfoData(string accessToken);
        Task<IdentityUser> GetIdentityUser(UserInfoResponse userInfoData);
    }


}
