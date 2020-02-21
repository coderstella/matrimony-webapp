using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebApp.Data.Entities;

namespace WebApp.Data.Extentions
{
    public static class DbContextExtension
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this ApplicationContext context)
        {
            //Seed Roles
            List<AppRole> roles = new List<AppRole>();
            roles.Add(new AppRole { Name = "Administrator", NormalizedName = "Administrator", ConcurrencyStamp = Guid.NewGuid().ToString() });
            roles.Add(new AppRole { Name = "Member", NormalizedName = "Member", ConcurrencyStamp = Guid.NewGuid().ToString() });
            context.Roles.AddRange(roles);
            context.SaveChanges();

            // Seed demo user
            List<AppUser> users = new List<AppUser>();
            users.Add(new AppUser { FullName = "Demo User", UserName = "KG0000001", NormalizedUserName = "KG0000001", Email = "demo@demo.com", NormalizedEmail = "demo@demo.com", EmailConfirmed = true, PasswordHash = GetHashPassword("Demo+123"), ConcurrencyStamp = Guid.NewGuid().ToString() });
            users.Add(new AppUser { FullName = "Test User", UserName = "KG0000002", NormalizedUserName = "KG0000002", Email = "test@test.com", NormalizedEmail = "test@test.com", EmailConfirmed = true, PasswordHash = GetHashPassword("Test+123"), ConcurrencyStamp = Guid.NewGuid().ToString() });
            context.Users.AddRange(users);
            context.SaveChanges();

            // Seed user role
            var memberRole = roles.Find(r => r.Name == "Member");
            var singleUserId = users.Find(u => u.FullName == "Test User");
            IdentityUserRole<string> identityUserRole = new IdentityUserRole<string> { RoleId = memberRole.Id, UserId = singleUserId.Id };
            context.UserRoles.Add(identityUserRole);
            context.SaveChanges();

            // Seed portfolio type

            List<PortfolioType> portfolioTypes = new List<PortfolioType>();
            portfolioTypes.Add(new PortfolioType { Name = "Myself" });
            portfolioTypes.Add(new PortfolioType { Name = "Daughter" });
            portfolioTypes.Add(new PortfolioType { Name = "Son" });
            portfolioTypes.Add(new PortfolioType { Name = "Sister" });
            portfolioTypes.Add(new PortfolioType { Name = "Brother" });
            portfolioTypes.Add(new PortfolioType { Name = "Relative" });
            portfolioTypes.Add(new PortfolioType { Name = "Friend" });
            context.PortfolioTypes.AddRange(portfolioTypes);
            context.SaveChanges();
        }

        private static string GetHashPassword(string passwordstring)
        {
            byte[] salt;
            byte[] bytes;
            if (passwordstring == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(passwordstring, 16, 1000))
            {
                salt = rfc2898DeriveByte.Salt;
                bytes = rfc2898DeriveByte.GetBytes(32);
            }
            byte[] numArray = new byte[49];
            Buffer.BlockCopy(salt, 0, numArray, 1, 16);
            Buffer.BlockCopy(bytes, 0, numArray, 17, 32);
            return Convert.ToBase64String(numArray);
        }
    }
}
