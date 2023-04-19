using Domain.Enums.RecipeEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RecipeEntities
{
    public  class Region
    {
        public int Id { get; set; }
        public RegionEnum RegionName { get; set; }
    }
}
