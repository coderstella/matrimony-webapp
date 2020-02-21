using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.Dtos
{
    public class PortfolioDetailsDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Profession { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string AppUserId { get; set; }
        public Guid PortfolioTypeId { get; set; }
        public Guid PhotoId { get; set; }
        public IEnumerable<PhotoDto> Photos { get; set; }
    }
}
