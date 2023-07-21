using Domain.Entities.UserEntities;

namespace Application.Interfaces.UserRepo
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task AddUserAsync(ApplicationUser applicationUser);
        Task UpdateUserAsync(ApplicationUser applicationUser); // Added UpdateUserAsync method
    }
}
