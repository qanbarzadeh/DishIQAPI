using Domain.Entities.UserEntities;

namespace Domain.Entities.RecipeEntities
{
    public class RecipeRequest
    {
        public int? MealTypeId { get; set; }
        public MealType MealType { get; set; }

        public int? DietPreferenceId { get; set; }
        public DietPreference DietPreference { get; set; }

        public int? RegionId { get; set; }
        public Region Region { get; set; }

        public int? CookingTechniqueId { get; set; }
        public CookingTechnique CookingTechnique { get; set; }

        public List<int> FlavorIds { get; set; } = new List<int>();
        public List<Flavor> Flavors { get; set; } = new List<Flavor>();

        public int? NumberOfPax { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }

        public int? MealTimeId { get; set; }
        public MealTime MealTime { get; set; }

        public List<int> DislikeIds { get; set; } = new List<int>();
        public List<Dislike> Dislikes { get; set; } = new List<Dislike>();

        public int? BloodTypeId { get; set; }
        public BloodType? BloodType { get; set; }
    }
}
