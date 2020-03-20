using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Data.Entities;
using WebApp.Data.Interfaces;
using WebApp.Dtos;
using WebApp.Services.Interfaces;

namespace WebApp.Data.Resolvers
{
    public class PortfolioInterestResolver : IValueResolver<Portfolio, PortfolioUserDetailsDto, bool>
    {
        
        private readonly ILogger<PortfolioInterestResolver> _logger;
        private readonly IMemberInterestRepository _memberInterestRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public PortfolioInterestResolver(ILogger<PortfolioInterestResolver> logger, 
            IMemberInterestRepository memberInterestRepository, 
            UserManager<AppUser> userManager, 
            IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _memberInterestRepository = memberInterestRepository;
            _userManager = userManager;
            _httpContext = httpContext;
        }        
        
        public bool Resolve(Portfolio source, PortfolioUserDetailsDto destination, bool destMember, ResolutionContext context)
        {
            try
            {                
                var appUser = _userManager.GetUserAsync(_httpContext.HttpContext.User).GetAwaiter().GetResult();
                var fromId = appUser.Portfolio.Id;
                var toId = source.Id;

                var result = _memberInterestRepository.CheckRequested(fromId.ToString(), toId.ToString()).GetAwaiter().GetResult();
                if (result != null)
                    return true;
                
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occur in Interest Resolver: {0} at {1}", ex.Message, DateTime.UtcNow);
                throw new Exception(ex.Message);
            }
        }
    }
}
