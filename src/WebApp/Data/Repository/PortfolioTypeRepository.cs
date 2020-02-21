using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;
using WebApp.Data.Interfaces;

namespace WebApp.Data.Repository
{
    public class PortfolioTypeRepository : IPortfolioTypeRepository
    {
        private readonly ApplicationContext _context;

        private readonly ILogger<PortfolioTypeRepository> _logger;
        public PortfolioTypeRepository(ApplicationContext context, ILogger<PortfolioTypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<PortfolioType>> GetAllAsync()
        {
            try
            {
                var portfolioTypes = await _context.PortfolioTypes.ToListAsync();
                if (portfolioTypes != null)
                    return portfolioTypes;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in PortfolioType Reposioty while getting all data: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<PortfolioType> GetByIdAsync(string id)
        {
            try
            {
                var portfolioType = await _context.PortfolioTypes.SingleOrDefaultAsync(t => t.Id == Guid.Parse(id));
                if (portfolioType != null)
                    return portfolioType;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in PortfolioType Reposioty while getting all data: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }
    }
}
