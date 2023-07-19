using Application.Interfaces.UserRepo;
using Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly AppDbContext _context;

        public ApplicationUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(int id)
        {
            var user = await _context.ApplicationUser.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} not found");
            }

            return user;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            var users = await _context.ApplicationUser.ToListAsync();
            if (users.Count == 0)
            {
                throw new Exception("No users found");
            }

            return users;
        }

        public async Task AddUserAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Provided user is null");
            }

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to add user", e);
            }
        }

        public async Task UpdateUserAsync(ApplicationUser applicationUser)
        {
            
            _context.Users.Update(applicationUser);
             await _context.SaveChangesAsync();
        }

        // Implement additional methods as needed
    }
}
