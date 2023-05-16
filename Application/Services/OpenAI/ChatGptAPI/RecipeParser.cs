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

            var responseObject = JObject.Parse(message);
            var content = responseObject["content"]?.ToString();

            if (string.IsNullOrEmpty(content))
            {
                throw new Exception("Invalid JSON format: 'content' is empty or null.");
            }

            var foodInformation = ParseFoodInformation(content);
            var ingredients = ParseIngredients(content);
            var cookingSteps = ParseCookingSteps(content);

            var generatedRecipeDTO = new GeneratedRecipeDTO
            {
                FoodInformation = foodInformation,
                Ingredients = ingredients,
                CookingSteps = cookingSteps
            };

            return generatedRecipeDTO;
        }

        public FoodInformationDTO ParseFoodInformationFromJson(string json)
        {
            JObject jObject = JObject.Parse(json);

            var message = jObject["choices"]?[0]?["message"]?["content"]?.ToString();

            if (string.IsNullOrEmpty(message))
            {
                throw new Exception("Invalid JSON format: Message content is empty or null.");
            }

            var lines = message.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var foodInformation = new FoodInformationDTO();

            foreach (var line in lines)
            {
                if (line.StartsWith("-"))
                {
                    var keyValue = line.TrimStart('-').Split(':');

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
                }
            }

            return foodInformation;
        }

        public List<IngredientDTO> ParseIngredients(string content)
        {
            JObject jObject = JObject.Parse(content);
            var message = jObject["choices"][0]["message"]["content"].ToString();

            // Parse the list of ingredients
            var ingredientsIndex = message.Contains("List of Ingredients:") ? message.IndexOf("List of Ingredients:") : -1;
            var cookingStepsIndex = message.Contains("Cooking Steps:") ? message.IndexOf("Cooking Steps:") : -1;

            if (ingredientsIndex == -1 || cookingStepsIndex == -1)
            {
                throw new Exception("Invalid message format. Couldn't find List of Ingredients or Cooking Steps.");
            }

            var ingredientsString = message.Substring(ingredientsIndex, cookingStepsIndex - ingredientsIndex).Trim();

            var ingredientsLines = ingredientsString.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None).Skip(1);
            List<IngredientDTO> ingredients = new List<IngredientDTO>();

            foreach (var line in ingredientsLines)
            {
                var parts = line.Split(",");

                if (parts.Length < 2)
                {
                    throw new Exception("Invalid ingredient format. Expected at least two parts: name and quantity.");
                }

                IngredientDTO ingredient = new IngredientDTO
                {
                    Name = parts[0].Trim(),
                    Quantity = parts[1].Trim()
                };

                if (parts.Length >= 3)
                {
                    ingredient.Unit = parts[2].Trim();
                }

                ingredients.Add(ingredient);
            }

            return ingredients;
        }

        public List<CookingStepDTO> ParseCookingSteps(string content)
        {
            JObject jObject = JObject.Parse(content);
            var message = jObject["choices"][0]["message"]["content"].ToString();

            // Parse the cooking steps
            var cookingStepsIndex = message.Contains("Cooking Steps:") ? message.IndexOf("Cooking Steps:") : -1;

            if (cookingStepsIndex == -1)
            {
                throw new Exception("Invalid message format. Couldn't find Cooking Steps.");
            }

            var cookingStepsString = message.Substring(cookingStepsIndex).Trim();

            var cookingStepsLines = cookingStepsString.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None).Skip(1);
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
            return ParseFoodInformationFromJson(content);
        }

    }
}

       