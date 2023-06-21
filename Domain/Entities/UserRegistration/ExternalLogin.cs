using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserRegistration
{
    public class ExternalLogin
    {
        public int Id { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime LinkedAt { get; set; } = DateTime.UtcNow;
    }
}
