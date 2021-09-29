using System.Collections.Generic;

namespace Backend.Core.Models.DTOs.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public bool Succes { get; set; }
        public List<string> Errors { get; set; }
    }
}