using Domain.Enums;

namespace Domain.Entities.UserEntities
{
    public class UserCookingSkillLevel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public CookingSkillLevelEnum CookingSkillLevel { get; set; }

    }
}
