using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public virtual UserProfileInfo UserProfileInfo { get; set; }
        public virtual UserCredentials UserCredentials { get; set; }
        public virtual ICollection<UserAllergy> UserAllergies { get; set; }
        public virtual ICollection<UserCookingSkillLevel> UserCookingSkillLevels { get; set; }
        public virtual ICollection<DietaryPreferencesEnum> DietaryPreferences { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; set; }
        public virtual UserActivityLog UserActivityLog { get; set; }

    }
}
