using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums.RecipeEnums
{
    public enum FlavorEnum
    {
        [Description("None")]
        None = 0,

        [Description("Sweet")]
        Sweet = 1,

        [Description("Sour")]
        Sour = 2,

        [Description("Salty")]
        Salty = 3,

        [Description("Bitter")]
        Bitter = 4,

        [Description("Umami")]
        Umami = 5
    }

}
