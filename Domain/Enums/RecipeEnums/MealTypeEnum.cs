using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
