using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserCookingSkillLevel
    {
        //[Key] move to FluentAPI 
        public int UserCookingSkillLevelId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public CookingSkillLevelEnum CookingSkillLevel { get; set; }

        // Navigation property to User
        public virtual User User { get; set; }

    }
}
