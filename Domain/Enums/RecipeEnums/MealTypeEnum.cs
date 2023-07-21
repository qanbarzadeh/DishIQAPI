using System.ComponentModel;

namespace Domain.Enums.RecipeEnums
{
    public enum MealTypeEnum
    {

        [Description("Main Course")]
        MainCourse = 1,

        [Description("Appetizer")]
        Appetizer,

        [Description("Dessert")]
        Dessert,

        [Description("Side Dish")]
        SideDish,

        [Description("Soup")]
        Soup,

        [Description("Salad")]
        Salad
    }
}
