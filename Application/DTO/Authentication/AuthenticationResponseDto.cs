using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Authentication
{
    public class AuthenticationResponseDto
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
    }
}
