using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Authentication.Manual
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
