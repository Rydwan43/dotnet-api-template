using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Core.Configuration;
using Backend.Core.Interfaces;
using Backend.Core.Models.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManagementService _authService;
        private readonly JwtConfig _jwtConfig;

        public AuthController(
            IAuthManagementService authService,
            IOptionsMonitor<JwtConfig> optionsMonitor
        )
        {
            _authService = authService;
            _jwtConfig = optionsMonitor.CurrentValue;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] AuthRegistrationRequest user)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.Register(user);

                return Ok(response);
            }

            return BadRequest(new AuthResponse(){
                Errors = new List<string>() {
                    "Invalid payload"
                },
                Succes = false
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.Login(user);
                return Ok(response);
            }

            return BadRequest(new AuthResponse(){
                Errors = new List<string>() {
                    "Invalid payload"
                },
                Succes = false
            });
        }
        
    }
}