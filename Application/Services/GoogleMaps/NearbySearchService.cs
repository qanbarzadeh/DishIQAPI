using GoogleApi.Entities.Places.Search.NearBy.Request;
using GoogleApi.Entities.Places.Search.Common.Enums;
using GoogleApi.Entities.Common;
using Application.DTO.GoogleMaps;
using Application.Interfaces.GoogleMaps;
using System.Net.Http;
using Application.Interfaces.Services; // I assumed this is the namespace for IKeyVaultService
using System.Threading;
using System.Threading.Tasks;
using Domain.AzureVault;

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
        var splitLocation = location.Split(',');
        var latitude = double.Parse(splitLocation[0]);
        var longitude = double.Parse(splitLocation[1]);
        var coordinate = new Coordinate(latitude, longitude);

        var storeList = new StoreListDTO { Stores = new List<StoreDTO>() };

        foreach (var type in types)
        {
            var request = new PlacesNearBySearchRequest
            {
                Key = await GoogleApiKey(),
                Location = coordinate,
                Radius = radius,
                Type = (SearchPlaceType)System.Enum.Parse(typeof(SearchPlaceType), type)
            };

            // Directly calling the ExecuteAsync method
            var response = await GoogleApi.GooglePlaces.NearBySearch.QueryAsync(request);

            foreach (var result in response.Results)
            {
                storeList.Stores.Add(new StoreDTO
                {
                    Name = result.Name,
                    Address = result.Vicinity,
                    Latitude = result.Geometry.Location.Latitude,
                    Longitude = result.Geometry.Location.Longitude
                });
            }
        }

        return storeList;
    }
}
