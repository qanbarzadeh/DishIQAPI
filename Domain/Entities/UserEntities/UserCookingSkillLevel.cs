using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.UserEntities
{
    public class UserCookingSkillLevel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public CookingSkillLevelEnum CookingSkillLevel { get; set; }

    }
}
