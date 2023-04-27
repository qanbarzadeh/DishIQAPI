namespace Application.DTO.RecipeDTOs
{
    public class GeneratedRecipeDTO
    { 
        public FoodInformationDTO FoodInformation { get; set; } = new FoodInformationDTO();
        public List<IngredientDTO> Ingredients { get; set; } = new List<IngredientDTO>();
        public List<CookingStepDTO> CookingSteps { get; set; } = new List<CookingStepDTO>();
    }
}
