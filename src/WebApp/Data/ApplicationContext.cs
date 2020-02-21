using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Data.Entities;
using WebApp.Data.Interfaces;

namespace WebApp.Data
{
    public class ApplicationContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public DbSet<Portfolio> Portfolios { get; set; }

        public DbSet<PortfolioType> PortfolioTypes { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<MemberInterest> MemberInterests { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder) 
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<MemberInterest>()
        //        .HasOne(mi => );

        //}

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}
