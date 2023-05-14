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
        private readonly IConfiguration? _configuration;
        private readonly HttpClient _httpClient;
        private readonly IRecipeParser _recipeParser;
        private readonly ILogger _logger;
        private const string _apiHost = "https://openai80.p.rapidapi.com";
        private const string _rapidApikey = "87469d0de2msh68ef681a9b461f3p1bfa82jsn173917c29b0e";

        public ChatGptService(HttpClient httpClient,
            IConfiguration configuration,
            ILogger<ChatGptService> logger,
            IRecipeParser recipeParser)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _recipeParser = recipeParser;
            _logger = logger;
            _httpClient.BaseAddress = new Uri(_apiHost);
            _httpClient.BaseAddress = new Uri(_configuration["OPENAI_API_URL"]); // Assuming you store the OpenAI API URL in appsettings.json
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration["OPENAI_API_KEY"]}");
            _httpClient.Timeout = TimeSpan.FromSeconds(60); // or any desired duration
                                                            //logging 
            _logger.LogInformation("ChatGptService initialized");
            _logger.LogInformation($"_apiHost: {_apiHost}");
            //_logger.LogInformation($"_rapidApiKey: {_rapidApikey}");
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

            var requestUri = new Uri("https://openai80.p.rapidapi.com/chat/completions");
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = content
            };
            _logger.LogInformation($"Sending API request to {_apiHost} with prompt: {prompt}"); //logging 

            try
            {
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                // Log the response content and headers
                _logger.LogInformation($"Response Content: {await response.Content.ReadAsStringAsync()}");
                _logger.LogInformation($"Response Headers: {response.Headers.ToString()}");

                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseJson);
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
