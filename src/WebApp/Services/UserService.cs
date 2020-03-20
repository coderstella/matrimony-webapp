using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.Data.Interfaces;
using WebApp.Dtos;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        public UserService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IPortfolioRepository portfolioRepository,  SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _portfolioRepository = portfolioRepository;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task<UserDeatailsDto> GetByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.Portfolio = await _portfolioRepository.GetByUserId(user.Id);
            
            if (user == null)
                return null;

            return _mapper.Map<UserDeatailsDto>(user);
        }
        public async Task<AppUser> GetUserByEmail()
        {
            var currentUserEmail = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value);
            if (currentUserEmail == null)
                return null;

            return currentUserEmail;
        }
        public async Task<AppUser> GetUserByGender()
        {
            var userGender = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Gender)?.Value);
            if (userGender == null)
                return null;

            return userGender;
        }
    }
}
