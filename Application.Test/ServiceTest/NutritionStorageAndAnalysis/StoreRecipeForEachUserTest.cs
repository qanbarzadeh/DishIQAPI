using Application.DTO.RecipeDTOs;
using Application.Interfaces.UnitOfWork;
using Application.Services.UsersLinkRecipes;
using AutoMapper;
using Domain.Entities.RecipeEntities;
using Domain.Entities.UserEntities;
using Moq;
using Xunit;

namespace Application.Test.ServiceTest.NutritionStorageAndAnalysis
{
    public class UserSpecificRecipeStorageServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapper;
        private readonly UserSpecificRecipeStorageService _service;

        public UserSpecificRecipeStorageServiceTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _service = new UserSpecificRecipeStorageService(_unitOfWork.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddUserWithRecipe_ThrowsException_WhenApplicationUserIsNull()
        {
            // Arrange
            ApplicationUser user = null;
            var generatedRecipe = new GeneratedRecipeDTO();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddUserWithRecipe(user, generatedRecipe));
        }

        [Fact]
        public async Task AddUserWithRecipe_ThrowsException_WhenRecipeIsNull()
        {
            // Arrange
            var user = new ApplicationUser();
            GeneratedRecipeDTO recipe = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddUserWithRecipe(user, recipe));
        }

        [Fact]
        public async Task AddUserWithRecipe_ShouldSaveUserWithRecipe_WhenValidInputs()
        {
            // Arrange
            var user = new ApplicationUser();
            var recipeDto = new GeneratedRecipeDTO();
            var recipe = new Recipe();

            _mapper.Setup(x => x.Map<Recipe>(recipeDto)).Returns(recipe);
            _unitOfWork.Setup(x => x.RecipeRepository.AddRecipeAsync(recipe)).Returns(Task.CompletedTask);
            _unitOfWork.Setup(x => x.UserRepository.AddUserAsync(user)).Returns(Task.CompletedTask);
            _unitOfWork.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(1));

            // Act
            await _service.AddUserWithRecipe(user, recipeDto);

            // Assert
            _unitOfWork.Verify(x => x.RecipeRepository.AddRecipeAsync(recipe), Times.Once);
            _unitOfWork.Verify(x => x.UserRepository.AddUserAsync(user), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
