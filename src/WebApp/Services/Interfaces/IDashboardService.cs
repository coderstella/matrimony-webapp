using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Dtos;

namespace WebApp.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<IEnumerable<PortfolioUserDetailsDto>> GetAllAsync(string currentUserProfileId, string currentUserGender);
        Task<string> AddInterestAsync(MemberInterestDto interest);
    }
}
