using Application.Interfaces.NutritionsAnalysis;
using Application.Interfaces.UserRepo;

namespace Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {

        IRecipeRepository RecipeRepository { get; }
        IRecipeIngredientRepository RecipeIngredientRepository { get; }
        IApplicationUserRepository UserRepository { get; }
        INutritionInformationRepository NutritionInformationRepository { get; } // New addition
                                                                                // Add additional repositories as needed
        Task<int>  SaveChangesAsync();
    
    }
}   
