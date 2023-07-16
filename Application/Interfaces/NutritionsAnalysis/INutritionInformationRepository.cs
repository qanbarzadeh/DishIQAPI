using Domain.Entities.RecipeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.NutritionsAnalysis
{
    public interface INutritionInformationRepository
    {
        Task<NutritionInformation> GetNutritionInformationByIdAsync(int id);
        Task<IEnumerable<NutritionInformation>> GetAllNutritionInformationsAsync();
        Task AddNutritionInformationAsync(NutritionInformation nutritionInformation);
        // Add other necessary methods here
    }

}
