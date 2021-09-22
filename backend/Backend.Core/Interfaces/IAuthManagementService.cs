using System.Threading.Tasks;
using Backend.Core.Configuration;
using Backend.Core.Models.DTOs.Requests;
using Backend.Core.Models.DTOs.Responses;
using Microsoft.AspNetCore.Identity;

namespace Backend.Core.Interfaces
{
    public interface IAuthManagementService
    {
        Task<RegistrationResponse> Register(UserRegistrationDto user);
        Task<RegistrationResponse> Login(UserLoginRequest user);
        string GenerateJwtToken(IdentityUser user);
    }
}