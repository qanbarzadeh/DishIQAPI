using Newtonsoft.Json;

namespace Application.DTO.RecipeDTOs
{
    public class CookingStepDTO
    {


        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }
    }
}
