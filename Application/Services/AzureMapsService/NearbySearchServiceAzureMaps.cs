using Application.DTO.Azure.maps;
using Application.DTO.GoogleMaps;
using Application.Interfaces.Azure.Maps;
using Domain.AzureVault;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Application.Services.AzureMaps
{
    public class NearbySearchServiceAzureMaps : INearbySearchServiceAzureMaps
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IKeyVaultService _keyVaultService;
        private string _azureMapsApiKey;
        private readonly string _azureMapsBaseUrl;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public NearbySearchServiceAzureMaps(IHttpClientFactory httpClientFactory, IKeyVaultService keyVaultService, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
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

            var httpClient = _httpClientFactory.CreateClient();
            var requestUrl = $"{_azureMapsBaseUrl}?subscription-key={await AzureMapsApiKey()}&api-version=1.0&query=grocery%20store&lat={searchRequestDTO.Latitude}&lon={searchRequestDTO.Longitude}&radius={searchRequestDTO.Radius}";
            var response = await httpClient.GetAsync(requestUrl);

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
