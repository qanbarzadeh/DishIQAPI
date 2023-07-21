using Application.DTO.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Authentication.Helpers
{
    public interface IEntityCreationService
    {
        Task HandleUserEntitiesCreation(string provider, UserInfoResponse userInfoData, IdentityUser identityUser);
    }

}
