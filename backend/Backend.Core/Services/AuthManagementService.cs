using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Backend.Core.Configuration;
using Backend.Core.Interfaces;
using Backend.Core.Models.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Core.Services
{
    public class AuthManagementService : IAuthManagementService
    {
        private readonly UserManager<IdentityUser> _userManager;
         private readonly JwtConfig _jwtConfig;
        public AuthManagementService(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHanlder = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHanlder.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHanlder.WriteToken(token);

            return jwtToken;
        }

        public async Task<AuthResponse> Login(AuthLoginRequest user)
        {
            var existingUser = await _userManager.FindByNameAsync(user.Username);
            if (existingUser is null)
                {
                    return new AuthResponse(){
                        Errors = new List<string>() {
                            "Invalid username"
                        },
                        Succes = false
                    };
                }
            
            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

            if (!isCorrect)
                {
                    return new AuthResponse(){
                        Errors = new List<string>() {
                            "Invalid password"
                        },
                        Succes = false
                    };
                }
            
            var jwtToken = GenerateJwtToken(existingUser);

            return new AuthResponse(){
                    Succes = true,
                    Token = jwtToken
                };
        }

        public async Task<AuthResponse> Register(AuthRegistrationRequest user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return new AuthResponse(){
                            Errors = new List<string>() {
                            "Email already in use"
                        },
                        Succes = false
                    };
                }

                var newUser = new IdentityUser() { Email = user.Email, UserName = user.Username };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);

                    return new AuthResponse() {
                        Succes = true,
                        Token = jwtToken
                    };
                } else {
                    return new AuthResponse(){
                            Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                            Succes = false
                    };
                }
        }
    }
}