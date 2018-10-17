using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MusicStore.Web.Models;

namespace MusicStore.Web
{
    public class SeedData
    {
        public static void Initialize(ApplicationDbContext modelContext)
        {
            if (!modelContext.Database.Exists())
            {
                throw new ArgumentException("Can not find dataBase, connection is wrong");
            }

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(modelContext));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(modelContext));

            var admin = new ApplicationUser {Email = "admin@come.ua", UserName = "admin@come.ua"};
            string password = "admin@come.ua";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, "Admin");
            }
            modelContext.SaveChanges();
        }
    }
}
