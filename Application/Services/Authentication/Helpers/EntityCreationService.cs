using Application.DTO.Authentication;
using Application.Interfaces.Authentication.Helpers;
using Application.Interfaces.UnitOfWork;
using Domain.Entities.Factories.UserRegistration;
using Domain.Enums.UserRegistration;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Authentication.Helpers
{
    public class EntityCreationService : IEntityCreationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EntityCreationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleUserEntitiesCreation(string provider, UserInfoResponse userInfoData, IdentityUser identityUser)
        {
            var authUser = UserFactory.CreateUser(identityUser.Email, identityUser.UserName);
            var externalLogin = ExternalLoginFactory.CreateExternalLogin(provider, userInfoData.Id, authUser);
            var userEvent = UserEventFactory.CreateUserEvent(authUser, EventType.Login);

            // Add the entities to the repositories.
            await _unitOfWork.AuthUsers.AddAuthUserAsync(authUser);
            await _unitOfWork.ExternalLogins.AddExternalLoginAsync(externalLogin);
            await _unitOfWork.UserEvents.AddUserEventAsync(userEvent);

            // Save all entities in a single transaction.
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to create and store user-related entities", e);
            }
        }
    }

}
