using Application.DTO.Azure.maps;
using Application.Interfaces.Azure.Maps;
using Domain.AzureVault;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Application.Services.AzureMaps
{
    public class NearbySearchServiceAzureMaps : INearbySearchServiceAzureMaps
    {
        private readonly HttpClient _httpClient;
        private readonly IKeyVaultService _keyVaultService;
        private string _azureMapsApiKey;
        private readonly string _azureMapsBaseUrl;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public NearbySearchServiceAzureMaps(HttpClient httpClient, IKeyVaultService keyVaultService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _keyVaultService = keyVaultService;
            _azureMapsBaseUrl = configuration["AzureMaps:BaseUrl"];
        }


        private async Task<string> AzureMapsApiKey()
        {
            if (_azureMapsApiKey == null)
            {
                await _semaphore.WaitAsync();
                try
                {
                    if (_azureMapsApiKey == null)
                    {
                        _azureMapsApiKey = await _keyVaultService.GetSecretAsync("azure-maps-key");
                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            return _azureMapsApiKey;
        }

        public async Task<StoreListDTO> Search(SearchRequestDTO searchRequestDTO)
        {
            var storeList = new StoreListDTO { Stores = new List<StoreDTO>() };

            // No longer necessary to create a new HttpClient here. You already have _httpClient.
            //var httpClient = _httpClientFactory.CreateClient();

            var requestUrl = $"{_azureMapsBaseUrl}?subscription-key={await AzureMapsApiKey()}&api-version=1.0&query=grocery%20store&lat={searchRequestDTO.Latitude}&lon={searchRequestDTO.Longitude}&radius={searchRequestDTO.Radius}";

            var response = await _httpClient.GetAsync(requestUrl); // Use _httpClient here

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(responseContent);
                var results = data["results"].ToObject<List<JToken>>();

                foreach (var result in results)
                {
                    storeList.Stores.Add(new StoreDTO
                    {
                        Name = result["poi"]["name"].ToString(),
                        Address = result["address"]["freeformAddress"].ToString(),
                        Latitude = (double)result["position"]["lat"],
                        Longitude = (double)result["position"]["lon"]
                    });
                }
            }
            else
            {
                throw new HttpRequestException($"Azure Maps API request failed with status code: {response.StatusCode}");
            }

            return storeList;
        }

    }
}
