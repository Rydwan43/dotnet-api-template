using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Core.Configuration;
using Backend.Core.Interfaces;
using Backend.Core.Models.DTOs.Requests;
using Backend.Core.Models.DTOs.Responses;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.Register(user);

                return Ok(response);
            }

            return BadRequest(new RegistrationResponse(){
                Errors = new List<string>() {
                    "Invalid payload"
                },
                Succes = false
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.Login(user);
                return Ok(response);
            }

            return BadRequest(new RegistrationResponse(){
                Errors = new List<string>() {
                    "Invalid payload"
                },
                Succes = false
            });
        }
        
    }
}