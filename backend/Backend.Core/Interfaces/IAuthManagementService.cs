using System.Threading.Tasks;
using Backend.Core.Configuration;
using Backend.Core.Models.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace Backend.Core.Interfaces
{
    public interface IAuthManagementService
    {
        Task<AuthResponse> Register(AuthRegistrationRequest user);
        Task<AuthResponse> Login(AuthLoginRequest user);
        string GenerateJwtToken(IdentityUser user);
    }
}