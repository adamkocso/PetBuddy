using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetBuddy.Controllers.HomeController
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;

        public HomeController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Home()
        {
            return View();
        }
    }
}
