﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Areas.Members.ViewModels.Dashboard.Pages;
using WebApp.Handlers;
using WebApp.Services.Interfaces;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Http;
using WebApp.Dtos;
using System.Collections;

namespace WebApp.Areas.Members.Controllers
{
    [Authorize(Policy = "MemberRights")]
    [Area("Members")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IUserService _userService;
        private readonly ILogger<DashboardController> _logger;
        public DashboardController(IDashboardService dashboardService, IUserService userService, ILogger<DashboardController> logger)
        {
            _dashboardService = dashboardService;
            _userService = userService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = _userService.GetByUserId(userId).Result;
                var currentUserProfile = currentUser.Portfolio;

                List<PortfolioUserDetailsDto> portfolioList = new List<PortfolioUserDetailsDto>();

                if (currentUserProfile != null)
                       portfolioList = _dashboardService.GetAllAsync(currentUserProfile.Id, currentUserProfile.Gender).Result.ToList();

                var viewModel = new DashboardViewModel()
                {
                    SuggestedProfiles = portfolioList,
                    CurrentUserDetails = currentUser
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }            
        }

        //Member Interest Ajax
        [HttpPost]
        public async Task<IActionResult> AddInterestAjax([FromForm] MemberInterestDto interestForm)
        {
            if (interestForm == null)
                return BadRequest();

            var result = await _dashboardService.AddInterestAsync(interestForm);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}