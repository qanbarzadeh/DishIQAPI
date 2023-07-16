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
            // Check for null
            if (applicationUser== null)
                throw new ArgumentNullException(nameof(ApplicationUser));

            if (recipeDTO == null)
                throw new ArgumentNullException(nameof(recipeDTO));

            // Convert your DTO into the domain entity using AutoMapper
            Recipe recipe = _mapper.Map<Recipe>(recipeDTO);

            // Set user id in recipe
            recipe.UserId = applicationUser.UserId;

            // Save the Recipe to the database
            await _unitOfWork.RecipeRepository.AddRecipeAsync(recipe);

            // Save the user along with the new recipe
            // Assuming User has a Recipes property
            applicationUser.Recipes.Add(recipe);
            await _unitOfWork.UserRepository.AddUserAsync(applicationUser);

            // Finally save the changes to the database
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
