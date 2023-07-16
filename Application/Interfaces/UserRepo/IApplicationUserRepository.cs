using Domain.Entities.UserEntities;

namespace Application.Interfaces.UserRepo
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(int id);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task AddUserAsync(ApplicationUser applicationUser); 
        // Add other necessary methods here
    }
}
