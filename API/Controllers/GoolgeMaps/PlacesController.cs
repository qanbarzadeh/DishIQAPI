using Application.DTO.GoogleMaps;
using Application.Interfaces.GoogleMaps;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.GoolgeMaps
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : ControllerBase
    {
        private readonly INearbySearchService _nearbySearchService;

        public PlacesController(INearbySearchService nearbySearchService)
        {
            _nearbySearchService = nearbySearchService ?? throw new ArgumentNullException(nameof(nearbySearchService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(StoreListDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StoreListDTO>> SearchNearbyPlaces(string location, int radius, string[] types)
        {
            try
            {
                var storeList = await _nearbySearchService.Search(location, radius, types);
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
