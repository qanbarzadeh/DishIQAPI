using System.ComponentModel;


namespace Domain.Enums
{
    [Flags]
    public enum CookingSkillLevelEnum
    {
        [Description("1 - Beginner")]
        Beginner = 1,

        [Description("2 - Intermediate")]
        Intermediate = 2,

        [Description("3 - Advanced")]
        Advanced = 3

    }
}
