using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Application.DTO.RecipeDTOs;
using Application.Interfaces;
using Newtonsoft.Json.Linq;

namespace OpenAIAPI
{
    public class RecipeParser : IRecipeParser
    {
        public GeneratedRecipeDTO Parse(string content)
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
            var ingredientsString = message.Substring(ingredientsIndex, cookingStepsIndex - ingredientsIndex).Trim();
            var cookingStepsString = message.Substring(cookingStepsIndex).Trim();

            var foodLines = foodInformationString.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);

            FoodInformationDTO foodInformationDTO = new FoodInformationDTO
            {
                Name = foodLines.ElementAtOrDefault(0)?.Split(":")[1].Trim(),
                PreparationTime = foodLines.ElementAtOrDefault(1)?.Split(":")[1].Trim(),
                CookingTime = foodLines.ElementAtOrDefault(2)?.Split(":")[1].Trim(),
                Servings = foodLines.ElementAtOrDefault(3)?.Split(":")[1].Trim(),
                CaloriesPerServing = foodLines.ElementAtOrDefault(4)?.Split(":")[1].Trim(),
                ServingSize = foodLines.ElementAtOrDefault(5)?.Split(":")[1].Trim(),
                DietaryPreferences = foodLines.ElementAtOrDefault(6)?.Split(":")[1].Trim(),
                KeyIngredients = foodLines.ElementAtOrDefault(7)?.Split(":")[1].Trim(),
                AllergyRestrictions = foodLines.ElementAtOrDefault(8)?.Split(":")[1].Trim(),
                Cuisine = foodLines.ElementAtOrDefault(9)?.Split(":")[1].Trim(),
                DishType = foodLines.ElementAtOrDefault(10)?.Split(":")[1].Trim(),
                CookingMethod = foodLines.ElementAtOrDefault(11)?.Split(":")[1].Trim()
            };

            // Parse the Ingredients
            var ingredientsLines = ingredientsString.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None).Skip(1);
            List<IngredientDTO> ingredients = ingredientsLines.Select(line =>
            {
                var parts = line.Split(",");
                return new IngredientDTO
                {
                    Name = parts[0].Trim(),
                    Quantity = parts[1].Trim(),
                    Unit = parts.Length > 2 ? parts[2].Trim() : null
                };
            }).ToList();

            // Parse the Cooking Steps
            var cookingStepsLines = cookingStepsString.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None).Skip(1);
            List<CookingStepDTO> cookingSteps = cookingStepsLines.Select(line =>
            {
                var parts = line.Split(".");
                return new CookingStepDTO
                {
                    Order = parts[0].Trim(),
                    Description = parts[1].Trim()
                };
            }).ToList();

            // Populate the GeneratedRecipeDTO
            GeneratedRecipeDTO generatedRecipeDTO = new GeneratedRecipeDTO
            {
                FoodInformation = foodInformationDTO,
                Ingredients = ingredients,
                CookingSteps = cookingSteps
            };

            return generatedRecipeDTO;
        }
    }
}
