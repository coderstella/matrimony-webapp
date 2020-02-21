using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Dtos;

namespace WebApp.Areas.Members.ViewModels.Dashboard.Pages
{
    public class CurrentUserViewModel
    {
        public UserDeatailsDto CurrentUserDetails { get; set; }
        public IEnumerable<PortfolioDetailsDto> Portfolios { get; set; }
    }
}
