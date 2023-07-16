using Domain.Enums;

namespace Domain.Entities.UserEntities
{
    public class UserCookingSkillLevel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public CookingSkillLevelEnum CookingSkillLevel { get; set; }

    }
}
