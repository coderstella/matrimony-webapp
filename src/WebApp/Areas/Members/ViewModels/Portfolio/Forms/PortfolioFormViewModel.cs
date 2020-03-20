using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;
using WebApp.Dtos;

namespace WebApp.Areas.Members.ViewModels.Portfolio.Forms
{
    public class PortfolioFormViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Date of Bitrh")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }
        public string Profession { get; set; }
        public string Location { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string AppUserId { get; set; }

        public Guid PortfolioTypeId { get; set; }

        public List<PhotoDto> Photos { get; set; }
    }
}
