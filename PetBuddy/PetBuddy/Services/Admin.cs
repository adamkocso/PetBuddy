using Microsoft.AspNetCore.Identity;
using PetBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetBuddy.Services
{
    public class Admin
    {
        public static void CreateAdmin(UserManager<User> userManager)
        {
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                User user = new User
                {
                    UserName = "Admin",
                    Email = "admin&gmail.com"
                };

                IdentityResult check = userManager.CreateAsync(user, "Password123..").Result;

                if (check.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
