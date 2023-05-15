using Application.DTO.RecipeDTOs;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application.Services.OpenAI.ChatGptAPI
{
    public class RecipeParser : IRecipeParser
    {
        public GeneratedRecipeDTO Parse(string content)
        {
            JObject jObject = JObject.Parse(content);
            var message = jObject["choices"][0]["message"]["content"].ToString();

            // Parse the Food Information
            var foodInformationString = message.Substring(message.IndexOf("Food information"), message.IndexOf("List of ingredients")).Trim();

            var lines = foodInformationString.Split("\n");
            var name = lines[1].Split(":")[1].Trim();
            var description = lines[2].Split(":")[1].Trim();
            var preparationTime = lines[3].Split(":")[1].Trim();
            // Parse the rest of the fields in a similar way...

            FoodInformationDTO foodInformationDTO = new FoodInformationDTO
            {
                Name = name,
                Description = description,
                PreparationTime = preparationTime,
                // Assign rest of the parsed fields here...
            };

            // TODO: Parse the Ingredients and CookingSteps in a similar way.

            GeneratedRecipeDTO generatedRecipeDTO = new GeneratedRecipeDTO
            {
                FoodInformation = foodInformationDTO,
                // Assign parsed Ingredients and CookingSteps here
            };

            return generatedRecipeDTO;
        }
    }


}

