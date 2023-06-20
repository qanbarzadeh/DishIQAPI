using Application.DTO.Azure.maps;
using Application.Interfaces.Azure.Maps;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Maps.Azure.Maps
{
    [ApiController]
    [Route("api/[controller]")]
    public class AzureMapsController : ControllerBase
    {
        private readonly INearbySearchServiceAzureMaps _nearbySearchServiceAzureMaps;

        public AzureMapsController(INearbySearchServiceAzureMaps nearbySearchServiceAzureMaps)
        {
            _nearbySearchServiceAzureMaps = nearbySearchServiceAzureMaps ?? throw new ArgumentNullException(nameof(nearbySearchServiceAzureMaps));
        }

        [HttpPost]
        [ProducesResponseType(typeof(StoreListDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StoreListDTO>> SearchNearbyPlaces(double latitude, double longitude, double radius)
        {
            try
            {
                // Create a new SearchRequestDTO with the provided parameters
                var searchRequestDTO = new SearchRequestDTO
                {
                    Latitude = latitude,
                    Longitude = longitude,
                    Radius = radius
                };

                var storeList = await _nearbySearchServiceAzureMaps.Search(searchRequestDTO);
                return Ok(storeList);
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions specifically related to the HttpRequest
                return BadRequest($"An error occurred while fetching the places: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other general exceptions
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }
    }
}

