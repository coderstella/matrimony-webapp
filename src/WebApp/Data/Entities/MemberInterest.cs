using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Entities
{
    public class MemberInterest : BaseEntity
    {
        public string FromId { get; set; }
        public string ToId { get; set; }
    }
}
