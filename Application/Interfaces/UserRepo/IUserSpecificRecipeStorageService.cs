using Application.DTO.RecipeDTOs;
using Domain.Entities.UserEntities;

namespace Application.Interfaces.UserRepo
{
    public interface IUserSpecificRecipeStorageService
    {
        Task AddUserWithRecipe(ApplicationUser user, GeneratedRecipeDTO recipe);
    }
}