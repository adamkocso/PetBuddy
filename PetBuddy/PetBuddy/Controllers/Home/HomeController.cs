using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetBuddy.Models;
using PetBuddy.Services;
using PetBuddy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetBuddy.Controllers.HomeController
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly IUserService userService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        [HttpGet("/")]
        public IActionResult Home()
        {
            var places = homeService.FindAllPlacesAsync();
            return View(new HomeViewModel { Places = places });
        }
        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            await userService.Logout();
            return RedirectToAction(nameof(LoginController.Login), "Login");
        }
    }
}
