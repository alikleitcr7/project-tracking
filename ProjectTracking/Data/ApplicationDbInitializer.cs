using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data
{
    public class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@sys.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin@sys.com",
                    Email = "admin@sys.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "123123").Result;

                if (result.Succeeded)
                {
                    


                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

    }
}
