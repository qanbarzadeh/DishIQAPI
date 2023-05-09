using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.RecipeDTOs
{
    public class RecipeResponseDTO
    {
        public FoodInformationDTO FoodInformation { get; set; }
        public List<IngredientDTO> Ingredients { get; set; }
        public List<CookingStepDTO> CookingSteps { get; set; }
    }
}
