using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserCookingSkillLevel
    {
        public int UserCookingSkillLevelId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public CookingSkillLevelEnum CookingSkillLevel { get; set; }

        
    }
}
