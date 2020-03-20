using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;
using WebApp.Dtos;

namespace WebApp.Data.Resolvers
{
    public class PortfolioAgeResolver : IValueResolver<Portfolio, PortfolioUserDetailsDto, int>
    {
        private readonly ILogger<PortfolioAgeResolver> _logger;
        public PortfolioAgeResolver(ILogger<PortfolioAgeResolver> logger)
        {
            _logger = logger;

        }
        public int Resolve(Portfolio source, PortfolioUserDetailsDto destination, int destMember, ResolutionContext context)
        {
            try
            {
                DateTime now = DateTime.Today;
                var birthDate = source.BirthDate;
                var portpolioAge = now.Year - birthDate.Year;
                if (birthDate > now.AddYears(-portpolioAge))
                {
                    return portpolioAge--;
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
