using System;
using System.Threading.Tasks;
using Application.DTO.RecipeDTOs;
using Application.Interfaces.UnitOfWork;
using Application.Interfaces.UserRepo;
using AutoMapper;
using Domain.Entities.RecipeEntities;
using Domain.Entities.UserEntities;

namespace Application.Services.UsersLinkRecipes
{
    public class UserSpecificRecipeStorageService : IUserSpecificRecipeStorageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserSpecificRecipeStorageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddUserWithRecipe(ApplicationUser applicationUser, GeneratedRecipeDTO recipeDTO)
        {
            try
            {
                // Check for null
                if (applicationUser == null)
                    throw new ArgumentNullException(nameof(applicationUser));

                if (recipeDTO == null)
                    throw new ArgumentNullException(nameof(recipeDTO));

                // Convert your DTO into the domain entity using AutoMapper
                Recipe recipe = _mapper.Map<Recipe>(recipeDTO);

                // Link user to the recipe
                recipe.UserId = applicationUser.Id; // Set the UserId property of the recipe to the Id of the application user
                recipe.User = applicationUser;

                // Save the Recipe to the database
                await _unitOfWork.RecipeRepository.AddRecipeAsync(recipe);

                // Save the user along with the new recipe
                // Assuming User has a Recipes property
                applicationUser.Recipes.Add(recipe);
                await _unitOfWork.UserRepository.UpdateUserAsync(applicationUser); // Changed to Update because the user already exists

                // Finally save the changes to the database
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle your exception here, e.g. logging the error message
                throw; // This will re-throw the caught exception, you can customize this part according to your error handling strategy
            }
        }
    }
}
