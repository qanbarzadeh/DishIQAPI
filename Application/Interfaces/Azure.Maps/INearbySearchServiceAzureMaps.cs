using Application.DTO.Azure.maps;

namespace Application.Interfaces.Azure.Maps
{
    public interface INearbySearchServiceAzureMaps
    {
        Task<StoreListDTO> Search(SearchRequestDTO searchRequestDTO);
    }
}
