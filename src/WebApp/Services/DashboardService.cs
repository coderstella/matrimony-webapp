using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;
using WebApp.Data.Interfaces;
using WebApp.Dtos;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IMapper _mapper;
        private readonly IMemberInterestRepository _memberInterestRepository;
        private readonly ILogger<DashboardService> _logger;
        

        public DashboardService(IPortfolioRepository portfolioRepository, IMemberInterestRepository memberInterestRepository, IMapper mapper, ILogger<DashboardService> logger)
        {
            _portfolioRepository = portfolioRepository;
            _memberInterestRepository = memberInterestRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PortfolioUserDetailsDto>> GetAllAsync(string currentUserProfileId, string currentUserGender)
        {
            if (string.IsNullOrWhiteSpace(currentUserGender))
                return null;

            try
            {
                var result = await _portfolioRepository.GetByUserGender(currentUserGender);
                if (result == null)
                    return null;
                
                var portfolios = _mapper.Map<IEnumerable<PortfolioUserDetailsDto>>(result);
                var existingInterests = await _memberInterestRepository.GetExistingInterest(currentUserProfileId);

                portfolios.Select(p => p.IsRequested = existingInterests.Contains(p.Id) ? true : false);

                return portfolios;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur while getting all portfolios list: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> AddInterestAsync(MemberInterestDto interest)
        {
            try
            {                
                var memberInterest = _mapper.Map<MemberInterest>(interest);
                var result = await _memberInterestRepository.SaveAsync(memberInterest);
                if (string.IsNullOrEmpty(result))
                    return null;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while saving user interest request: {0} at {1}", ex.Message, DateTime.Now.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
