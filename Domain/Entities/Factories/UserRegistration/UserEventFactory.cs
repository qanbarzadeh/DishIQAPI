using Domain.Entities.UserRegistration;
using Domain.Enums.UserRegistration;

namespace Domain.Entities.Factories.UserRegistration
{
    public static class UserEventFactory
    {
        public static UserEvent CreateUserEvent(AuthUser user, EventType eventType)
        {
            return new UserEvent
            {
                AuthUserId = user.Id,
                AuthUser = user,
                EventType = eventType,
                EventDate = DateTime.UtcNow
            };
        }
    }
}
