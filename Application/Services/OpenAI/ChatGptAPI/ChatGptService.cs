using Application.DTO.RecipeDTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Application.Services.OpenAI.ChatGptAPI
{
    public class ChatGptService : IChatGptService
    {
        private readonly IConfiguration? _configuration;
        private readonly HttpClient _httpClient;
        private readonly string? _rapidApiKey;
        private readonly string? _rapidapiEndpoint;
        private readonly string? _apiHost;

        public ChatGptService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _rapidapiEndpoint = configuration["RapidAPI:ApiEndpoint"] ?? throw new System.ArgumentNullException(nameof(configuration), "RapidApi endPoint api endpoint cannot be null");
            _rapidApiKey = configuration["RapidAPI:ApiKey"] ?? throw new System.ArgumentNullException(nameof(configuration), "RapidAPI key cannot be null");
            _apiHost = configuration["RapidAPI:ApiHost"] ?? throw new System.ArgumentNullException(nameof(configuration), "RapidAPI endpoint cannot be null");
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _rapidApiKey);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _apiHost);
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            _httpClient.Timeout = TimeSpan.FromSeconds(60); // or any desired duration

        }

        public async Task<GeneratedRecipeDTO> GeneratedRecipeApiAsync(RecipeRequestDTO request)
        {
            // Build the prompt
            string prompt = BuildPrompt(request);

            // Prepare the request JSON
            //var requestData = new
            //{
            //    prompt,
            //    max_tokens = 512,
            //    temperature = 0.5,
            //    top_p = 1,
            //    frequency_penalty = 0,
            //    presence_penalty = 0,
            //    stop = "###"
            //};
            //Change for RapidAPI 
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

            // Send the HTTP request to the ChatGPT API
            var response = await _httpClient.PostAsync(_rapidapiEndpoint, content);
            response.EnsureSuccessStatusCode();

            // Deserialize the response JSON to GeneratedRecipe
            var responseJson = await response.Content.ReadAsStringAsync();
            var generatedRecipeDTO = JsonConvert.DeserializeObject<GeneratedRecipeDTO>(responseJson);

            return generatedRecipeDTO;
        }

        private string BuildPrompt(RecipeRequestDTO request)
        {
            var promptBuilder = new StringBuilder();
            promptBuilder.AppendLine($"I want to cook a {request.NumberOfPax}-serving {request.MealType} dish");
            promptBuilder.AppendLine($"that is {request.DietPreference} and suitable for {request.BloodType} blood type");
            promptBuilder.AppendLine($"from {request.Region} and {request.Country}");
            promptBuilder.AppendLine($"using {request.CookingTechnique} cooking technique for {request.MealTime}");
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
