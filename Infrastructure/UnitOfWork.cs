using Application.Interfaces.NutritionsAnalysis;
using Application.Interfaces.UnitOfWork;
using Application.Interfaces.UserRepo;
using System;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private Lazy<IRecipeRepository> _recipeRepository;
        private Lazy<IRecipeIngredientRepository> _recipeIngredientRepository;
        private Lazy<IApplicationUserRepository> _userRepository;
        private Lazy<INutritionInformationRepository> _nutritionInformationRepository;

        public UnitOfWork(AppDbContext context,
            Lazy<IRecipeRepository> recipeRepository,
            Lazy<IRecipeIngredientRepository> recipeIngredientRepository,
            Lazy<IApplicationUserRepository> userRepository,
            Lazy<INutritionInformationRepository> nutritionInformationRepository)
        {
            _context = context;
            _recipeRepository = recipeRepository;
            _recipeIngredientRepository = recipeIngredientRepository;
            _userRepository = userRepository;
            _nutritionInformationRepository = nutritionInformationRepository;
        }

        public IRecipeRepository RecipeRepository => _recipeRepository.Value;

        public IRecipeIngredientRepository RecipeIngredientRepository => _recipeIngredientRepository.Value;

        public IApplicationUserRepository UserRepository => _userRepository.Value;

        public INutritionInformationRepository NutritionInformationRepository => _nutritionInformationRepository.Value;

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw;
            }
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
