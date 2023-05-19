using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.IngredientSelectionOrderTest
{
    public class IngredientsControllerTests
    {
        private Mock<IIngredientsService> _ingredientsServiceMock;
        private IngredientsController _controller;

        public IngredientsControllerTests()
        {
            _ingredientsServiceMock = new Mock<IIngredientsService>();
            _controller = new IngredientsController(_ingredientsServiceMock.Object);
        }

        [Fact]
        public async Task PostIngredients_ReturnsExpectedStores()
        {
            // Arrange
            var ingredients = new List<string>
        {
            "Chicken breasts",
            "Olive oil",
            "Lemon",
            // Add the rest of the ingredients
        };

            var expectedStores = new List<StoreDTO>
        {
            new StoreDTO { Name = "Store A", Distance = 0.5, TotalCost = 15 },
            new StoreDTO { Name = "Store B", Distance = 1, TotalCost = 16 },
            // Add more stores if needed
        };

            _ingredientsServiceMock.Setup(service => service.GetStoresForIngredientsAsync(ingredients))
                .ReturnsAsync(expectedStores);

            // Act
            var result = await _controller.PostIngredients(ingredients);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<StoreDTO>>>(result);
            var returnValue = Assert.IsType<List<StoreDTO>>(actionResult.Value);
            Assert.Equal(expectedStores, returnValue);
        }
    }

}
