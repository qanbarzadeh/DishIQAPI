using Application.Interfaces.NutritionsAnalysis;
using Domain.Entities.RecipeEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class NutritionInformationRepository : INutritionInformationRepository
    {
        private readonly AppDbContext _context;

        public NutritionInformationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NutritionInformation> GetNutritionInformationByIdAsync(int id)
        {
            var nutritionInformation = await _context.NutritionInformation.FindAsync(id);
            if (nutritionInformation == null)
            {
                throw new KeyNotFoundException($"Nutrition Information with id {id} not found");
            }

            return await nutritionInformation;
        }

        public async Task<IEnumerable<NutritionInformation>> GetAllNutritionInformationAsync()
        {
            var nutritionInformation = await _context.NutritionInformation.ToListAsync();
            if (nutritionInformation.Count == 0)
            {
                throw new Exception("No nutrition information found");
            }

            return nutritionInformation;
        }

        public async Task AddNutritionInformationAsync(NutritionInformation nutritionInformation)
        {
            if (nutritionInformation == null)
            {
                throw new ArgumentNullException(nameof(nutritionInformation), "Provided nutrition information is null");
            }

            try
            {
                await _context.NutritionInformation.AddAsync(nutritionInformation);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to add nutrition information", e);
            }
        }

        public Task<IEnumerable<NutritionInformation>> GetAllNutritionInformationsAsync()
        {
            throw new NotImplementedException();
        }
        // Implement additional methods as needed
    }
}
