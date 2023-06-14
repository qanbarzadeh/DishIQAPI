using Application.DTO.GoogleMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.GoogleMaps
{
    public interface INearbySearchService
    {
        Task<StoreListDTO> Search(string location, int radius, string[] types);
    }

}
