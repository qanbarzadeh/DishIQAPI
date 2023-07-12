using Domain.Enums.RecipeEnums;

namespace Domain.Entities.RecipeEntities
{
    public class MealTime
    {
        public int Id { get; set; }
        public MealTimeEnum MealTimeEnum { get; set; } = MealTimeEnum.Breakfast;
    }
}
