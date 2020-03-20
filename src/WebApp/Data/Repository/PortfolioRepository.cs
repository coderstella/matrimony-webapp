using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;
using WebApp.Data.Interfaces;

namespace WebApp.Data.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationContext _context;

        private readonly ILogger<PortfolioRepository> _logger;
        public PortfolioRepository(ApplicationContext context, ILogger<PortfolioRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Portfolio>> GetAllAsync()
        {
            try
            {
                var portfolios = await _context.Portfolios.Include(p => p.Photos).ToListAsync();
                if (portfolios != null && portfolios.Count > 0)
                    return portfolios;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in portfolio while getting list of portfolios: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Portfolio>> GetByUserGender(string currentUserGender)
        {
            try
            {
                var portfolios = await _context.Portfolios.Where(p => p.Gender != currentUserGender).ToListAsync();
                if (portfolios != null)
                    return portfolios;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in portfolios while getting list of portfolios by user gender: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<Portfolio> GetByUserId(string userId)
        {
            try
            {
                var portfolio = await _context.Portfolios.Include(p => p.Photos).SingleOrDefaultAsync(p => p.AppUserId == userId);
                if (portfolio != null)
                    return portfolio;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in portfolios while getting list of portfolios by user id: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<Portfolio> GetOneAsync(string id)
        {
            var portfolio = await _context.Portfolios.FindAsync(Guid.Parse(id));
            if (portfolio != null)
                return portfolio;

            return null;
        }

        public async Task<Portfolio> GetSingleById(string id)
        {
            var portfolio = await _context.Portfolios.SingleOrDefaultAsync(p => p.Id == Guid.Parse(id));
            if (portfolio != null)
                return portfolio;

            return null;
        }

        public async Task<string> SaveAsync(Portfolio portfolio)
        {
            try
            {
                portfolio.User = _context.Users.Find(portfolio.AppUserId);

                _context.Entry(portfolio.User).State = EntityState.Unchanged;
                await _context.Portfolios.AddAsync(portfolio);
                _context.SaveChanges();

                return portfolio.Id.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(Portfolio portfolio)
        {
            try
            {
                _context.Portfolios.Update(portfolio);
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var portfolio = _context.Portfolios.SingleOrDefault(p => p.Id == Guid.Parse(id));
                _context.Portfolios.Remove(portfolio);
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
