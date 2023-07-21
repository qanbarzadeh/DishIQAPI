using Domain.Entities.UserEntities;
using Domain.Enums.UserRegistration;

namespace Domain.Entities.UserRegistration
{
    public class UserEvent
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } // Changed from Guid AuthUserId
        public ApplicationUser ApplicationUser { get; set; } // Changed from AuthUser
        public EventType EventType { get; set; }
        public DateTime EventDate { get; set; } = DateTime.UtcNow;
    }
}
