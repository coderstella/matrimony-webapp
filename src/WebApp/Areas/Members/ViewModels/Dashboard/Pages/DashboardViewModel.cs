using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Dtos;

namespace WebApp.Areas.Members.ViewModels.Dashboard.Pages
{
    public class DashboardViewModel
    {
        public IEnumerable<PortfolioUserDetailsDto> PortfolioDetails { get; set; }        
        public UserDeatailsDto CurrentUserDetails { get; set; }
    }
}
