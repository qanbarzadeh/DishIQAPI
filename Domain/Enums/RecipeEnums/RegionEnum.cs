using System.ComponentModel;

namespace Domain.Enums.RecipeEnums
{
    public enum RegionEnum
    {
        [Description("1. Asian Food")]
        Asian = 1,
        [Description("2. Middle Eastern Food")]
        MiddleEastern = 2,
        [Description("3. European Food")]
        European = 3,
        [Description("4. African Food")]
        African = 4,
        [Description("5. South American Food")]
        SouthAmerican = 5,
        [Description("6. North American Food")]
        NorthAmerican = 6,
        [Description("7. Australian Food")]
        Australian = 7
    }

}
