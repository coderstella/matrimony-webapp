using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Members.ViewModels.Dashboard.Pages;
using WebApp.Areas.Members.ViewModels.Portfolio.Forms;
using WebApp.Areas.Members.ViewModels.Portfolio.Pages;
using WebApp.Data.Entities;
using WebApp.Data.Resolvers;
using WebApp.Dtos;

namespace WebApp.Data
{
    public class DataProfile : Profile
    {
        public DataProfile() 
        {
            CreateMap<PortfolioFormViewModel, Portfolio>().ReverseMap(); // For Protfolio user form

            CreateMap<PortfolioDetailsDto, Portfolio>().ReverseMap(); // For Protfolio index page

            CreateMap<PortfolioUserDetailsDto, Portfolio>().ReverseMap(); // For Dashboard index page

            //CreateMap<Portfolio, PortfolioUserDetailsDto>();
                //.ForMember(dest => dest.IsRequested, opt => opt.MapFrom<PortfolioInterestResolver>());

            CreateMap<PortfolioTypeDto, PortfolioType>().ReverseMap();

            CreateMap<FileUploadDto, Photo>().ReverseMap();

            CreateMap<UserDeatailsDto, AppUser>().ReverseMap();

            CreateMap<PhotoDto, Photo>().ReverseMap();

            CreateMap<MemberInterestDto, MemberInterest>().ReverseMap();


        }
    }
}
