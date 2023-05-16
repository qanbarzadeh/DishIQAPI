using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Application.DTO.OpenAiResponse;
using Application.DTO.RecipeDTOs;
using Application.Interfaces;
using Newtonsoft.Json.Linq;

namespace OpenAIAPI
{
    public class RecipeParser : IRecipeParser
    {
        public FoodInformationDTO ParseFoodInformation(string content)
        {
            JObject jObject = JObject.Parse(content);
            var message = jObject["choices"][0]["message"]["content"].ToString();

            // Parse the Food Information
            var foodInformationIndex = message.Contains("Food Information:") ? message.IndexOf("Food Information:") : -1;
            var ingredientsIndex = message.Contains("List of Ingredients:") ? message.IndexOf("List of Ingredients:") : -1;
            var cookingStepsIndex = message.Contains("Cooking Steps:") ? message.IndexOf("Cooking Steps:") : -1;

            if (foodInformationIndex == -1 || ingredientsIndex == -1 || cookingStepsIndex == -1)
            {
                throw new Exception("Invalid message format. Couldn't find Food Information, List of Ingredients, or Cooking Steps.");
            }

            var foodInformationString = message.Substring(foodInformationIndex, ingredientsIndex - foodInformationIndex).Trim();

            var foodLines = foodInformationString.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);

            FoodInformationDTO foodInformationDTO = new FoodInformationDTO
            {
                Name = foodLines.ElementAtOrDefault(0)?.Split(":")[1]?.Trim(),
                PreparationTime = foodLines.ElementAtOrDefault(1)?.Split(":")[1]?.Trim(),
                CookingTime = foodLines.ElementAtOrDefault(2)?.Split(":")[1]?.Trim(),
                Servings = foodLines.ElementAtOrDefault(3)?.Split(":")[1]?.Trim(),
                CaloriesPerServing = foodLines.ElementAtOrDefault(4)?.Split(":")[1]?.Trim(),
                ServingSize = foodLines.ElementAtOrDefault(5)?.Split(":")[1]?.Trim(),
                DietaryPreferences = foodLines.ElementAtOrDefault(6)?.Split(":")[1]?.Trim(),
                KeyIngredients = foodLines.ElementAtOrDefault(7)?.Split(":")[1]?.Trim(),
                AllergyRestrictions = foodLines.ElementAtOrDefault(8)?.Split(":")[1]?.Trim(),
                Cuisine = foodLines.ElementAtOrDefault(9)?.Split(":")[1]?.Trim(),
                DishType = foodLines.ElementAtOrDefault(10)?.Split(":")[1]?.Trim(),
                CookingMethod = foodLines.ElementAtOrDefault(11)?.Split(":")[1]?.Trim()
            };

            return foodInformationDTO;

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

    }
}

       