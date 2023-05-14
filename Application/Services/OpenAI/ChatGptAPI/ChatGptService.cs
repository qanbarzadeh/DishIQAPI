using Application.Configuration;
using Application.DTO.RecipeDTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Application.Interfaces;
using Application.DTO.OpenAiResponse;

namespace Application.Services.OpenAI.ChatGptAPI
{
    public class ChatGptService : IChatGptService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IRecipeParser _recipeParser;
        private readonly ILogger _logger;

        public ChatGptService(HttpClient httpClient,
            IConfiguration configuration,
            ILogger<ChatGptService> logger,
            IRecipeParser recipeParser)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _recipeParser = recipeParser;
            _logger = logger;
            _httpClient.BaseAddress = new Uri(_configuration["OpenAI:ApiEndpoint"]); // OpenAI API URL from appsettings.json
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAI_API_KEY")}"); // OpenAI API Key from environment variable
            _httpClient.Timeout = TimeSpan.FromSeconds(60); // or any desired duration
            _logger.LogInformation("ChatGptService initialized");
            _logger.LogInformation($"OpenAI API Endpoint: {_httpClient.BaseAddress}");
        }

        public async Task<GeneratedRecipeDTO> GeneratedRecipeApiAsync(RecipeRequestDTO recipeRequest)
        {
            // Build the prompt
            string prompt = BuildPrompt(recipeRequest);

            // Prepare the request JSON
            var requestData = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
            new
            {
                role = "user",
                content = prompt
            }
        }
            };

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var content = new StringContent(JsonConvert.SerializeObject(requestData, settings), Encoding.UTF8, "application/json");

            var requestUri = new Uri($"{_httpClient.BaseAddress}/v1/chat/completions");
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = content
            };
            _logger.LogInformation($"Sending API request to {_httpClient.BaseAddress} with prompt: {prompt}");

            try
            {
                HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);

                // Log the response content and headers
                _logger.LogInformation($"Response Content: {await response.Content.ReadAsStringAsync()}");
                _logger.LogInformation($"Response Headers: {response.Headers.ToString()}");

                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseJson);

                // Handle the scenario where the API response does not contain any choices
                if (apiResponse?.Choices == null || !apiResponse.Choices.Any())
                {
                    // Handle the error accordingly
                    throw new Exception("Invalid API response: No choices available.");
                }

                var assistantMessage = apiResponse.Choices.First().Message.Content;

                // Use the RecipeParser to parse the assistant message
                GeneratedRecipeDTO generatedRecipe = _recipeParser.Parse(assistantMessage);

                return generatedRecipe;
            }
            catch (HttpRequestException ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while calling the API");

                // Re-throw the exception to be handled by the caller
                throw;
            }
        }


        private string BuildPrompt(RecipeRequestDTO recipeRequest)
        {
            var promptBuilder = new StringBuilder();
            promptBuilder.AppendLine($"I want to cook a {recipeRequest.NumberOfPax}-serving {recipeRequest.MealType} dish");
            promptBuilder.AppendLine($"that is {recipeRequest.DietPreference} and suitable for {recipeRequest.BloodType} blood type");
            promptBuilder.AppendLine($"from {recipeRequest.Region} and {recipeRequest.Country}");
            promptBuilder.AppendLine($"using {recipeRequest.CookingTechnique} cooking technique for {recipeRequest.MealTime}");
            promptBuilder.AppendLine();
            promptBuilder.AppendLine("Please provide the following details for the generated recipe:");
            promptBuilder.AppendLine("- Food information (name, description, preparation time, cooking time," +
                " servings, calories per serving, serving size, dietary preferences, key ingredients, allergy restrictions, cuisine, " +
                "dish type, cooking method)");
            promptBuilder.AppendLine("- List of ingredients (name, unit, quantity)");
            promptBuilder.AppendLine("- Cooking steps (description, order)");
            promptBuilder.AppendLine("###");

            return promptBuilder.ToString();
        }

    }
}
