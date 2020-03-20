using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using WebApp.Areas.Members.ViewModels.Portfolio.Forms;
using WebApp.Areas.Members.ViewModels.Portfolio.Pages;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.Handlers;
using WebApp.Services;
using WebApp.Services.Interfaces;
using WebApp.Dtos;

namespace WebApp.Areas.Members.Controllers
{
    [Authorize(Policy = "MemberRights")]
    [Area("Members")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IFileHandler _fileHandler;
        private readonly ILogger<PortfolioController> _logger;
        private readonly IUserService _userService;
        public PortfolioController(IPortfolioService portfolioService, IFileHandler fileHandler, ILogger<PortfolioController> logger, IUserService userService)
        {
            _portfolioService = portfolioService;
            _fileHandler = fileHandler;
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var portfolio = _portfolioService.GetByUserId(currentUserId).Result;
            var portfolioTypes = _portfolioService.GetAllTypesAsync().Result;
            
            var isReadOnly = false;

            var createForm = new PortfolioFormViewModel()
            {
                BirthDate = DateTime.Now,
                AppUserId = currentUserId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            if(portfolio != null)
            {
                createForm = new PortfolioFormViewModel
                {
                    Id = portfolio.Id,
                    FullName = portfolio.FullName,
                    Gender = portfolio.Gender,
                    BirthDate = portfolio.BirthDate,
                    Profession = portfolio.Profession,
                    Location = portfolio.Location,
                    AppUserId = portfolio.AppUserId,
                    PortfolioTypeId = portfolio.PortfolioTypeId,
                    CreatedDate = portfolio.CreatedDate,
                    UpdatedDate = portfolio.UpdatedDate,
                    Photos = portfolio.Photos
                };

                isReadOnly = true;
            }

            var viewModel = new PortfolioPageViewModel() 
            { 
                PortfolioTypes = portfolioTypes,
                CreateForm = createForm,
                FormIsReadonly = isReadOnly
            };
            return View(viewModel);
        }

        //public IActionResult New()
        //{
        //    var portfolioTypes = _portfolioService.GetAllTypesAsync().Result;
        //    var currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        //    var viewModel = new AddPortfolioPageViewModel
        //    {
        //        PortfolioTypes = portfolioTypes,
        //        CreateForm = new PortfolioFormViewModel() 
        //        { 
        //            BirthDate = DateTime.Now,
        //            AppUserId = currentUserId
        //        },                
        //    };
        //    ViewData["ReturnUrl"] = "Members/Portfolio";
        //    return View(viewModel);
        //}

        public IActionResult Edit(string id)
        {
            var portfolio = _portfolioService.EditById(id).Result;
            var viewModel = new EditPortfolioPageViewModel
            {
                CreateForm = portfolio
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save(PortfolioFormViewModel createForm)
        {
            if(!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Some error occur! please try again";
                return RedirectToAction("Index", "Portfolio");
            }
            
            if(createForm.Id == null)
            {   
                var portfolioId = _portfolioService.SavePortfolioAsync(createForm).GetAwaiter().GetResult();
                if (!string.IsNullOrEmpty(portfolioId))
                {
                    TempData["SuccessMessage"] = "Profile created successfully!";
                    return RedirectToAction("Index", "Portfolio");
                }
            }
            if (createForm.Id != null) 
            {
                createForm.UpdatedDate = DateTime.Now;
                var updateResult = _portfolioService.UpdateAsync(createForm).Result;
                if(updateResult)
                    return RedirectToAction("Index", "Portfolio");

                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Delete(string portfolioId)
        {
            try
            {
                var id = portfolioId;
                var result = _portfolioService.DeleteAsync(id).Result;
                return RedirectToAction("Index", "Portfolio");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult UploadPhotosAjax([FromForm] PhotoUploadForm form)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (userEmail == null || form.portfolioId == null)
                return BadRequest();
            
            var uploadedFiles = _fileHandler.UploadFiles(form.Files, userEmail);
            var result = _portfolioService.SavePhotos(uploadedFiles, form.portfolioId).Result;

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetPhotosAjax(string portfolioId)
        {
            if (portfolioId == null)
                return BadRequest();

            var photos = _portfolioService.GetAllPhotosAsync(portfolioId).Result;
            if (photos == null)
                return BadRequest();

            return Ok(photos);
        }

        [HttpDelete]
        public IActionResult DeletePhotoAjax(string photoId, string name, string directory)
        {
            if (photoId == null || name == null || directory == null)
                return BadRequest();

            var result = _fileHandler.RemoveFile(name, directory);
            if(result)
            {
                var isDeleted = _portfolioService.DeletePhotoById(photoId).Result;
                return Ok(isDeleted);
            }
            return Ok(false);
        }

        [HttpGet]
        public IActionResult GetCurrentUserNameAjax()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userService.GetByUserId(userId).GetAwaiter().GetResult();
            
            if (user == null)
                return BadRequest();

            if(string.IsNullOrWhiteSpace(user.FullName))
                return NotFound();

            return Ok( new { FullName = user.FullName });
        }
    }
}