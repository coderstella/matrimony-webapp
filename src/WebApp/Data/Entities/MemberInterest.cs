using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Entities
{
    public class MemberInterest : BaseEntity
    {
        public Guid FromId { get; set; }
        public virtual Portfolio FromPortfolio { get; set; }

        public Guid ToId { get; set; }
        public virtual Portfolio ToPortfolio { get; set; }
    }
}
