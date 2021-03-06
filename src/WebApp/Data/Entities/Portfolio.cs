﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Entities
{
    public class Portfolio : BaseEntity
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Profession { get; set; }
        public string Location { get; set; }


        [ForeignKey("AppUserId")]
        public string AppUserId { get; set; }
        public AppUser User { get; set; }


        [ForeignKey("PortfolioTypeId")]
        public Guid PortfolioTypeId { get; set; }
        public PortfolioType PortfolioType { get; set; }  
        
        public List<Photo> Photos { get; set; }

        public virtual ICollection<MemberInterest> ToMemberInterests { get; set; }
        public virtual ICollection<MemberInterest> FromMemberInterests { get; set; }
    }
}
