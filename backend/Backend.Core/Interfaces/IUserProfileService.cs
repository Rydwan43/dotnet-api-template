using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core.Models.Helpers;

namespace Backend.Core.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileModel> GetProfileInfo();
    }
}