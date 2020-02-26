using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Dtos
{
    public class PortfolioMemberInterestDto
    {
        public string PortfolioId { get; set; }
        public ICollection<MemberInterestFromDto> FromInterest { get; set; }
        public ICollection<MemberInterestToDto> ToInterest { get; set; }
    }
}
