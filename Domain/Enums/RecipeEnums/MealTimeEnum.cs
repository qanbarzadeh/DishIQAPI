using System.ComponentModel;

namespace Domain.Enums.RecipeEnums
{
    public enum MealTimeEnum
    {
        [Description("Breakfast")]
        Breakfast = 1,

        [Description("Brunch")]
        Brunch = 2,

        [Description("Lunch")]
        Lunch = 3,

        [Description("Dinner")]
        Dinner = 4,

        [Description("Snack")]
        Snack = 5
    }
}
