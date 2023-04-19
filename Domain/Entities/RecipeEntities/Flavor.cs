using Domain.Enums.RecipeEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RecipeEntities
{
    public class Flavor
    {
        public int Id { get; set; }
        //public string Name { get; set; } = "None";
        public FlavorEnum FlavorType { get; set; } = FlavorEnum.None;
    }
}
