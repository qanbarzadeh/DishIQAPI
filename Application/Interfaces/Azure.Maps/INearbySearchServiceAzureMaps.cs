using Application.DTO.Azure.maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Azure.Maps
{
    public interface INearbySearchServiceAzureMaps
    {
            Task<StoreListDTO> Search(SearchRequestDTO searchRequestDTO);
    }
}
