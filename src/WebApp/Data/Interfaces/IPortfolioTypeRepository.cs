using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.Data.Interfaces
{
    public interface IPortfolioTypeRepository
    {
        Task<IEnumerable<PortfolioType>> GetAllAsync();

        Task<PortfolioType> GetByIdAsync(string id);
    }
}
