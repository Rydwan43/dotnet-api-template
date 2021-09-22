using System.Collections.Generic;

namespace Backend.Core.Configuration
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Succes { get; set; }
        public List<string> Errors { get; set; }
    }
}