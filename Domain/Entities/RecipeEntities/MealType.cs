using Domain.Enums.RecipeEnums;

namespace Domain.Entities.RecipeEntities
{
    public class MealType
    {
        public int Id { get; set; }
        public MealTypeEnum MealName { get; set; }
    }
}
