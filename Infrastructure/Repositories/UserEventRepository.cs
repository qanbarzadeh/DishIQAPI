using Application.Repository.Authentication;
using Domain.Entities.UserRegistration;

namespace Infrastructure.Repositories
{
    public class UserEventRepository : IUserEventRepository
    {
        private readonly AppDbContext _context;

        public UserEventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserEvent> AddUserEventAsync(UserEvent userEvent)
        {
            _context.UserEvents.Add(userEvent);
            await _context.SaveChangesAsync();
            return userEvent;
        }

        // Implement other methods as needed.
    }

}
