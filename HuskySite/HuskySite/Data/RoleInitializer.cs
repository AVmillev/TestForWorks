using HuskySite.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuskySite
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminName = "admin";
            string adminEmail = "avmillev@gmail.com";
            string password = "ZZ!g3426";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("HaskyCinema") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("HaskyCinema"));
            }
            if (await roleManager.FindByNameAsync("Bookkeeping") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Bookkeeping"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                ApplicationUser admin = new ApplicationUser { Email = adminEmail, UserName = adminName };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
