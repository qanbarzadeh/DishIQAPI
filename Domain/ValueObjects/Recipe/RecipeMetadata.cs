using Domain.Entities.RecipeEntities;

namespace Domain.ValueObjects.Recipe
{
    public class RecipeMetadata
    {
        public Country Country { get; set; }
        public int CountryID { get; set; }
        public Region Region { get; set; }
        public int RegionID { get; set; }
        public MealType MealType { get; set; }
        public int MealTypeID { get; set; }
        public MealTime MealTime { get; set; }
        public int MealTimeID { get; set; }
        public CookingTechnique CookingTechnique { get; set; }
        public int CookingTechniqueID { get; set; }
        public RecipeDietPreference DietPreference { get; set; }
        public int DietPreferenceID { get; set; }
    }
}
