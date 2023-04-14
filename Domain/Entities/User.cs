using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string EmailAddress { get; set; }

        // Navigation properties to related entities
        public virtual UserProfileInfo UserProfileInfo { get; set; } = new UserProfileInfo();
        public virtual UserCredentials UserCredentials { get; set; } = new UserCredentials(); 
        public virtual ICollection<UserAllergy> UserAllergies { get; set; } = new List<UserAllergy>();
        public virtual UserCookingSkillLevel UserCookingSkillLevel { get; set; } = new UserCookingSkillLevel();

        public virtual ICollection<DietaryPreferences> DietaryPreferences { get; set; } = new List<DietaryPreferences>();
        public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
        public virtual UserActivityLog UserActivityLog { get; set; } = new UserActivityLog();
    }
}
