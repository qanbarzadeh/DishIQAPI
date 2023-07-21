using Domain.Entities.UserRegistration;

namespace Application.Repository.Authentication
{
    public interface IExternalLoginRepository
    {
        Task<ExternalLogin> AddExternalLoginAsync(ExternalLogin externalLogin);
        Task<IEnumerable<ExternalLogin>> GetExternalLoginsByAuthUserIdAsync(Guid authUserId);
    }
}
