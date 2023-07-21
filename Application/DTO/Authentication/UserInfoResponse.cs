using Newtonsoft.Json;

namespace Application.DTO.Authentication
{
    public class UserInfoResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        // Add other fields as needed based on the data returned by the API
    }
}
