//using Application.DTO.RecipeDTOs;
//using Application.Interfaces.UnitOfWork;
//using Application.Services.UsersLinkRecipes;
//using AutoMapper;
//using Domain.Entities.RecipeEntities;
//using Domain.Entities.UserEntities;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace Application.Test
//{
//    public class UserSpecificRecipeStorageServiceTests
//    {
//        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
//        private readonly Mock<IMapper> _mockMapper;

//        public UserSpecificRecipeStorageServiceTests()
//        {
//            _mockUnitOfWork = new Mock<IUnitOfWork>();
//            _mockMapper = new Mock<IMapper>();
//        }

//        [Fact]
//        public async void AddUserWithRecipe_AddsRecipeToUser()
//        {
//            // Arrange
//            var user = new  { UserId = "testUser" };
//            var recipeDTO = new GeneratedRecipeDTO { FoodInformation = new FoodInformationDTO { Name = "Test Recipe" } };
//            var recipe = new Recipe { Name = "Test Recipe", UserId = user.UserId };
//            _mockMapper.Setup(m => m.Map<Recipe>(recipeDTO)).Returns(recipe);
//            _mockUnitOfWork.Setup(u => u.RecipeRepository.AddRecipeAsync(recipe));
//            _mockUnitOfWork.Setup(u => u.UserRepository.AddUserAsync(user));
//            _mockUnitOfWork.Setup(u => u.SaveChangesAsync());

//            var service = new UserSpecificRecipeStorageService(_mockUnitOfWork.Object, _mockMapper.Object);

//            // Act
//            await service.AddUserWithRecipe(user, recipeDTO);

//            // Assert
//            _mockUnitOfWork.Verify(u => u.RecipeRepository.AddRecipeAsync(recipe), Times.Once);
//            _mockUnitOfWork.Verify(u => u.UserRepository.AddUserAsync(user), Times.Once);
//            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
//        }
//    }
//}