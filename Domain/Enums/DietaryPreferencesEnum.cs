using System.ComponentModel;

namespace Domain.Enums
{
    [Flags]
    public enum DietaryPreferencesEnum
    {
        [Description("1. Vegan")]
        Vegan = 1,
        [Description("2. Vegetarian")]
        Vegetarian = 2,
        [Description("3. Gluten-free")]
        GlutenFree = 3,
        [Description("4. Dairy-free")]
        DairyFree = 4,
        [Description("5. Nut-free")]
        NutFree = 5,
        [Description("6. Kosher")]
        Kosher = 6,
        [Description("7. Halal")]
        Halal = 7
    }
}
