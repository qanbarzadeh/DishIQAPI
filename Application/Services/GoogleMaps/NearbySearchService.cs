using Application.DTO.GoogleMaps;
using Application.Interfaces.GoogleMaps;
using Domain.AzureVault;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;

public class NearbySearchService : INearbySearchService
{
    private readonly HttpClient _httpClient;
    private readonly IKeyVaultService _keyVaultService;
    private string _googleApiKey;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public NearbySearchService(HttpClient httpClient, IKeyVaultService keyVaultService)
    {
        _httpClient = httpClient;
        _keyVaultService = keyVaultService;
    }

    private async Task<string> GoogleApiKey()
    {
        if (_googleApiKey == null)
        {
            await _semaphore.WaitAsync();
            try
            {
                if (_googleApiKey == null)
                {
                    _googleApiKey = await _keyVaultService.GetSecretAsync("Google-Maps-API-Key");
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
        return _googleApiKey;
    }

    public async Task<StoreListDTO> Search(string location, int radius, string[] types)
    {
        var storeList = new StoreListDTO { Stores = new List<StoreDTO>() };

        foreach (var type in types)
        {
            var requestUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={location}&radius={radius}&type={type}&key={await GoogleApiKey()}";
            var response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(responseContent);
                var results = data["results"].ToObject<List<JToken>>();

                foreach (var result in results)
                {
                    storeList.Stores.Add(new StoreDTO
                    {
                        Name = result["name"].ToString(),
                        Address = result["vicinity"].ToString(),
                        Latitude = (double)result["geometry"]["location"]["lat"],
                        Longitude = (double)result["geometry"]["location"]["lng"]
                    });
                }
            }
            else
            {
                throw new HttpRequestException($"Google Places API request failed with status code: {response.StatusCode}");
            }
        }

        return storeList;
    }
}
