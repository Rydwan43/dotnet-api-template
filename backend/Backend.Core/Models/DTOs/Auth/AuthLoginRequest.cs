using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models.DTOs.Auth
{
    public class AuthLoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}