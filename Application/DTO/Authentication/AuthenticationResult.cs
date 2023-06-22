using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Authentication
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserId { get; set; }
        public IEnumerable<string>? Errors { get; set; } // nullable
    }

}
