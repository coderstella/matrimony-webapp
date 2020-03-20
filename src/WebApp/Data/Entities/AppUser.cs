using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}
