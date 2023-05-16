using Domain.Entities.RecipeEntities;
using Domain.Entities.UserEntities;
using Domain.Enums;

namespace Application.DTO.RecipeDTOs
{
    public class RecipeRequestDTO
    {
        public string MealType { get; set; }
        public string DietaryPreference { get; set; }
        public string Region { get; set; }
        public string CookingTechnique { get; set; }
        public int    NumberOfPax { get; set; }
        public string Country { get; set; }
        public string MealTime { get; set; }
        public string BloodType { get; set; }
        
    }

}
