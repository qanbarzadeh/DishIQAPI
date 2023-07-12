using Domain.Enums.UserRegistration;

namespace Domain.Entities.UserRegistration
{
    public class UserEvent
    {
        public int Id { get; set; }
        public Guid AuthUserId { get; set; }
        public AuthUser AuthUser { get; set; }
        public EventType EventType { get; set; }
        public DateTime EventDate { get; set; } = DateTime.UtcNow;
    }
}
