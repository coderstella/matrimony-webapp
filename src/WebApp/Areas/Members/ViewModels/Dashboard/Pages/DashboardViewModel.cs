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
        public ICollection<PortfolioUserDetailsDto> SuggestedProfiles { get; set; }        
        public UserDeatailsDto CurrentUserDetails { get; set; }
        public bool IsRequested { get; set; }
    }
}
