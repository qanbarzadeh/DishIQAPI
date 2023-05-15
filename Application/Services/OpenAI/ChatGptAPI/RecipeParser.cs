using Application.DTO.RecipeDTOs;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application.Services.OpenAI.ChatGptAPI
{
    public class RecipeParser : IRecipeParser
    {

        private readonly ILogger _logger;

        public RecipeParser(ILogger<RecipeParser> logger)
        {
            _logger = logger;
        }

        public GeneratedRecipeDTO Parse(string assistantMessage)
        {
            var lines = assistantMessage.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var generatedRecipe = new GeneratedRecipeDTO();
            
            return generatedRecipe;
        }
    }
}
    
