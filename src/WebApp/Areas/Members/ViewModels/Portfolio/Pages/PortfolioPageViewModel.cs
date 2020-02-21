using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Members.ViewModels.Portfolio.Forms;
using WebApp.Dtos;

namespace WebApp.Areas.Members.ViewModels.Portfolio.Pages
{
    public class PortfolioPageViewModel
    {
        public PortfolioFormViewModel CreateForm { get; set; }
        public IEnumerable<PortfolioTypeDto> PortfolioTypes { get; set; }
        public bool FormIsReadonly { get; set; }
    }
}
