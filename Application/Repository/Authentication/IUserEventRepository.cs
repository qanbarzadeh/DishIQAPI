using Domain.Entities.UserRegistration;

namespace Application.Repository.Authentication
{
    public interface IUserEventRepository
    {
        Task<UserEvent> AddUserEventAsync(UserEvent userEvent);

    }
}
