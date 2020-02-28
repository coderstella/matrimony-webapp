using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.Data.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<IEnumerable<Portfolio>> GetAllAsync();
        Task<IEnumerable<Portfolio>> GetByUserGender(string currentUserGender);
        Task<IEnumerable<Portfolio>> GetByUserId(string userId);
        Task<Portfolio> GetOneAsync(string id);
        Task<Portfolio> GetSingleById(string id);
        Task<string> SaveAsync(Portfolio portfolio);
        Task<bool> UpdateAsync(Portfolio portfolio);
        Task<bool> DeleteAsync(string id);
    }
}
