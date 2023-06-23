using Application.DTO.Authentication;
using Application.Interfaces.Authentication.Helpers;
using Application.Repository.Authentication;
using Domain.Entities.Factories.UserRegistration;
using Domain.Enums.UserRegistration;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Authentication.Helpers
{
    public class EntityCreationService : IEntityCreationService
    {
        private readonly IAuthUserRepository _authUserRepository;
        private readonly IExternalLoginRepository _externalLoginRepository;
        private readonly IUserEventRepository _userEventRepository;

        public EntityCreationService(IAuthUserRepository authUserRepository, IExternalLoginRepository externalLoginRepository, IUserEventRepository userEventRepository)
        {
            _authUserRepository = authUserRepository;
            _externalLoginRepository = externalLoginRepository;
            _userEventRepository = userEventRepository;
        }

        public async Task HandleUserEntitiesCreation(string provider, UserInfoResponse userInfoData, IdentityUser identityUser)
        {
            var authUser = UserFactory.CreateUser(identityUser.Email, identityUser.UserName);
            var externalLogin = ExternalLoginFactory.CreateExternalLogin(provider, userInfoData.Id, authUser);
            var userEvent = UserEventFactory.CreateUserEvent(authUser, EventType.Login);

            try
            {
                await _authUserRepository.AddAuthUserAsync(authUser);
                await _externalLoginRepository.AddExternalLoginAsync(externalLogin);
                await _userEventRepository.AddUserEventAsync(userEvent);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to create and store user-related entities", e); 
            }
        }
    }
}
