using Domain.Entities.UserRegistration;
using Domain.Enums.UserRegistration;
using Domain.Entities.UserEntities;

namespace Domain.Entities.Factories.UserRegistration
{
    public static class UserEventFactory
    {
        public static UserEvent CreateUserEvent(ApplicationUser user, EventType eventType)
        {
            return new UserEvent
            {
                ApplicationUserId = user.Id,
                ApplicationUser = user,
                EventType = eventType,
                EventDate = DateTime.UtcNow
            };
        }
    }
}
