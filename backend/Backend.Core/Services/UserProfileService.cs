using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Core.Interfaces;
using Backend.Core.Models.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Backend.Core.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public UserProfileService(
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContext
        )
        {
            _userManager = userManager;
            _httpContext = httpContext;
        }
        public async Task<UserProfileModel> GetProfileInfo()
        {
            var UserName = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByNameAsync(UserName);

            if (user is null)
            {
                throw new AccessViolationException(nameof(user));
            }

            var userProfile = new UserProfileModel {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email
            };

            return userProfile;
        }
    }
}