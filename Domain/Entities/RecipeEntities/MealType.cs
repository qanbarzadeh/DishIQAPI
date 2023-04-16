using Domain.Enums.RecipeEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.RecipeEntities
{
    public class MealType
    {
        public int Id { get; set; }
        public MealTypeEnum MealName { get; set; }
    }
}
