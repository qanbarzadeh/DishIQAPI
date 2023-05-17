using Newtonsoft.Json;

namespace Application.DTO.RecipeDTOs
{
    public class CookingStepDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }
    }
}
