using Application.DTO.Azure.maps;

namespace Application.Interfaces.GoogleMaps
{
    public interface INearbySearchService
    {
        Task<StoreListDTO> Search(string location, int radius, string[] types);
    }

}
