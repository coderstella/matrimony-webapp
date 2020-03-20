using AutoMapper;
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
    public class MemberInterestRepository : IMemberInterestRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<MemberInterestRepository> _logger;
        public MemberInterestRepository(ApplicationContext context, IMapper mapper, ILogger<MemberInterestRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<string>> GetExistingInterest(string currentPortfolioId)
        {
            try
            {
                List<string> ids = new List<string>();

                Guid tempGuid;
                if (!Guid.TryParse(currentPortfolioId, out tempGuid))
                    return null;

                var result = await _context.MemberInterests.Where(d => d.FromId == tempGuid).ToListAsync();
                foreach(var row in result)
                {
                    ids.Add(row.ToId.ToString());
                }

                if (ids != null)
                    return ids;

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MemberInterest> GetIdAsync(string id)
        {
            try
            {
                var result = await _context.MemberInterests.FindAsync(Guid.Parse(id));
                if (result != null)
                    return result;

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<MemberInterest> CheckRequested(string fromId, string toId)
        {
            try
            {
                var result = await _context.MemberInterests.SingleOrDefaultAsync(d => d.FromId == Guid.Parse(fromId) && d.ToId == Guid.Parse(toId));
                if (result != null)
                    return result;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occur while checking member interest: {0} at {1}", ex.Message, DateTime.UtcNow);
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SaveAsync(MemberInterest memberInterest)
        {
            try
            {
                _context.MemberInterests.Add(memberInterest);
                await _context.SaveChangesAsync();

                return memberInterest.Id.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }        
    }
}
