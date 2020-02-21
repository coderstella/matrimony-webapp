using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;
using WebApp.Dtos;

namespace WebApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDeatailsDto> GetByUserId(string userId);
        Task<AppUser> GetUserByEmail();
        Task<AppUser> GetUserByGender();
    }
}
