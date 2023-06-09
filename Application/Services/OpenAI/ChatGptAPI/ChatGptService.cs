using Application.Configuration;
using Application.DTO.RecipeDTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Application.Interfaces;
using Application.DTO.OpenAiResponse;
using Domain.Entities.RecipeEntities;
using System.Net.Http.Headers;
using Domain.AzureVault;

namespace Application.Services.OpenAI.ChatGptAPI
{
    public class ChatGptService : IChatGptService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IKeyVaultService _keyVaultService;
        private string _openAiApiKey;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public ChatGptService(HttpClient httpClient,
            ILogger<ChatGptService> logger,
            IConfiguration configuration,
            IKeyVaultService keyVaultService)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
            _keyVaultService = keyVaultService;
            _httpClient.Timeout = TimeSpan.FromSeconds(60);

            var openAiEndPoint = _configuration["OpenAI:ApiEndpoint"];
            _httpClient.BaseAddress = new Uri(openAiEndPoint);

            _logger.LogInformation("ChatGptService initialized");
            _logger.LogInformation($"OpenAI API Endpoint: {_httpClient.BaseAddress}");
        }

        private async Task<string> OpenAiApiKey()
        {
            if (_openAiApiKey == null)
            {
                await _semaphore.WaitAsync();
                try
                {
                    if (_openAiApiKey == null)
                    {
                        //_openAiApiKey = await _keyVaultService.GetSecretAsync("OpenAI-Key");
                        _openAiApiKey = await _keyVaultService.GetSecretAsync("openai-apikey-b");

                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            return _openAiApiKey;
        }

        public async Task<ApiResponseDTO> GeneratedRecipeApiAsync(RecipeRequestDTO recipeRequest)
        {
            var openAiApiKey = await OpenAiApiKey();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAiApiKey);

            string prompt = BuildPrompt(recipeRequest);
            var requestData = new
            {
                model = "gpt-3.5-turbo",
                max_tokens = 2048,
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

            var chatCompletionsEndpoint = _configuration["OpenAI:ChatCompletionsEndpoint"];
            var requestUri = new Uri(_httpClient.BaseAddress, chatCompletionsEndpoint);

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = content
            };

            try
            {
                HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseDTO>(responseJson);

                if (apiResponse?.Choices == null || !apiResponse.Choices.Any())
                {
                    throw new Exception("Invalid API response: No choices available.");
                }

                return apiResponse;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "An error occurred while calling the API");
                throw;
            }
        }
        private string BuildPrompt(RecipeRequestDTO recipeRequest)
        {
            var promptBuilder = new StringBuilder();
            promptBuilder.AppendLine("Please generate a recipe with the following criteria:");
            promptBuilder.AppendLine($"- Meal Type: {recipeRequest.MealType}");
            promptBuilder.AppendLine($"- Dietary Preference: {recipeRequest.DietaryPreference}");
            promptBuilder.AppendLine($"- Region: {recipeRequest.Region}");
            promptBuilder.AppendLine($"- Cooking Technique: {recipeRequest.CookingTechnique}");
            promptBuilder.AppendLine($"- Number of Pax: {recipeRequest.NumberOfPax}");
            promptBuilder.AppendLine($"- Country: {recipeRequest.Country}");
            promptBuilder.AppendLine($"- Meal Time: {recipeRequest.MealTime}");
            promptBuilder.AppendLine($"- Blood Type: {recipeRequest.BloodType}");
            promptBuilder.AppendLine("Please include the following details in the recipe:");
            promptBuilder.AppendLine("- Name");
            promptBuilder.AppendLine("- Description");
            promptBuilder.AppendLine("- Preparation Time");
            promptBuilder.AppendLine("- Cooking Time");
            promptBuilder.AppendLine("- Servings");
            promptBuilder.AppendLine("- Calories per serving");
            promptBuilder.AppendLine("- Serving Size");
            promptBuilder.AppendLine("- Dietary Preferences");
            promptBuilder.AppendLine("- Key Ingredients");
            promptBuilder.AppendLine("- Allergy Restrictions");
            promptBuilder.AppendLine("- Cuisine");
            promptBuilder.AppendLine("- Dish Type");
            promptBuilder.AppendLine("- Cooking Method");
            promptBuilder.AppendLine("- List of Ingredients");
            promptBuilder.AppendLine("- Cooking Steps");
            promptBuilder.AppendLine("###");

            return promptBuilder.ToString();
        }
    }
}
