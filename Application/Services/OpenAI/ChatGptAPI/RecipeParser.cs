using Application.DTO.OpenAiResponse;
using Application.DTO.RecipeDTOs;
using Application.Interfaces;
using Newtonsoft.Json.Linq;

namespace OpenAIAPI
{
    public class RecipeParser : IRecipeParser
    {
        public GeneratedRecipeDTO ParseApiResponse(ApiResponseDTO apiResponse)
        {
            if (apiResponse?.Choices == null || !apiResponse.Choices.Any())
            {
                throw new Exception("Invalid API response: No choices available.");
            }

            var message = apiResponse.Choices[0]?.Message?.Content;

            if (string.IsNullOrEmpty(message))
            {
                throw new Exception("Invalid message format: Empty message content.");
            }

            var foodInformation = ParseFoodInformation(message);
            var ingredients = ParseIngredients(message);
            var cookingSteps = ParseCookingSteps(message);

            var generatedRecipeDTO = new GeneratedRecipeDTO
            {
                FoodInformation = foodInformation,
                Ingredients = ingredients,
                CookingSteps = cookingSteps
            };

            return generatedRecipeDTO;
        }

        public FoodInformationDTO ParseFoodInformationFromContent(string content)
        {
            var lines = content.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var foodInformation = new FoodInformationDTO();

            foreach (var line in lines)
            {
                var keyValue = line.Split(':');

                if (keyValue.Length == 2)
                {
                    var key = keyValue[0].Trim();
                    var value = keyValue[1].Trim();

                    switch (key)
                    {
                        case "Name":
                            foodInformation.Name = value;
                            break;
                        case "Description":
                            foodInformation.Description = value;
                            break;
                        case "Preparation Time":
                            foodInformation.PreparationTime = value;
                            break;
                        case "Cooking Time":
                            foodInformation.CookingTime = value;
                            break;
                        case "Servings":
                            foodInformation.Servings = value;
                            break;
                        case "Calories per serving":
                            foodInformation.CaloriesPerServing = value;
                            break;
                        case "Serving Size":
                            foodInformation.ServingSize = value;
                            break;
                        case "Dietary Preferences":
                            foodInformation.DietaryPreferences = value;
                            break;
                        case "Key Ingredients":
                            foodInformation.KeyIngredients = value;
                            break;
                        case "Allergy Restrictions":
                            foodInformation.AllergyRestrictions = value;
                            break;
                        case "Cuisine":
                            foodInformation.Cuisine = value;
                            break;
                        case "Dish Type":
                            foodInformation.DishType = value;
                            break;
                        case "Cooking Method":
                            foodInformation.CookingMethod = value;
                            break;
                    }
                }
                else if (line.Contains("List of Ingredients:") || line.Contains("Cooking Steps:"))
                {
                    // Stop parsing as we've reached the end of the food information
                    break;
                }
            }

            return foodInformation;
        }



        public List<IngredientDTO> ParseIngredients(string content)
        {
            // Parse the list of ingredients
            var ingredientsIndex = content.IndexOf("List of Ingredients:", StringComparison.OrdinalIgnoreCase);
            var cookingStepsIndex = content.IndexOf("Cooking Steps:", StringComparison.OrdinalIgnoreCase);

            if (ingredientsIndex == -1 || cookingStepsIndex == -1)
            {
                throw new Exception("Invalid message format. Couldn't find List of Ingredients or Cooking Steps.");
            }

            // Adjust the starting index by skipping any consecutive newline characters
            var startIndex = content.LastIndexOf('\n', ingredientsIndex) + 1;

            var ingredientsString = content.Substring(startIndex, cookingStepsIndex - startIndex).Trim();

            var ingredientsLines = ingredientsString.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None).Skip(1);
            List<IngredientDTO> ingredients = new List<IngredientDTO>();

            foreach (var line in ingredientsLines)
            {
                IngredientDTO ingredient = new IngredientDTO
                {
                    IngredientInfo = line.Trim()
                };

                ingredients.Add(ingredient);
            }

            return ingredients;
        }




        public List<CookingStepDTO> ParseCookingSteps(string content)
        {
            // Parse the cooking steps
            var cookingStepsIndex = content.IndexOf("Cooking Steps:");

            if (cookingStepsIndex == -1)
            {
                throw new Exception("Invalid message format. Couldn't find Cooking Steps.");
            }

            var cookingStepsString = content.Substring(cookingStepsIndex).Trim();

            var cookingStepsLines = cookingStepsString.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Skip(1);
            List<CookingStepDTO> cookingSteps = new List<CookingStepDTO>();

            foreach (var line in cookingStepsLines)
            {
                var parts = line.Split(".");

                if (parts.Length < 2)
                {
                    throw new Exception("Invalid cooking step format. Expected at least two parts: order and description.");
                }

                CookingStepDTO cookingStep = new CookingStepDTO
                {
                    Order = parts[0].Trim(),
                    Description = parts[1].Trim()
                };

                cookingSteps.Add(cookingStep);
            }

            return cookingSteps;
        }

        public FoodInformationDTO ParseFoodInformation(string content)
        {
            return ParseFoodInformationFromContent(content);
        }

    }
}

     