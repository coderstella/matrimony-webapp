using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.Data.Interfaces
{
    public interface IMemberInterestRepository
    {
        Task<IEnumerable<string>> GetExistingInterest(string currentPortfolioId);
        Task<MemberInterest> GetIdAsync(string id);
        Task<MemberInterest> CheckRequested(string fromId, string toId);
        Task<string> SaveAsync(MemberInterest memberInterest);
    }
}
