using Domain.Enums.UserRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserRegistration
{
    public class UserEvent
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public AuthUser User { get; set; }
        public EventType EventType { get; set; }
        public DateTime EventDate { get; set; } = DateTime.UtcNow;
    }
}
