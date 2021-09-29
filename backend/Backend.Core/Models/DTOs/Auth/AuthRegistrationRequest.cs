using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models.DTOs.Auth
{
    public class AuthRegistrationRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}