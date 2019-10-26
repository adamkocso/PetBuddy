using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly IUserService userService;

        public HomeController(IHomeService homeService, IUserService userService)
        {
            this.homeService = homeService;
            this.userService = userService;
        }

        [HttpGet("/home")]
        public IActionResult Home()
        {
            var places = homeService.FindAllPlacesAsync();
            return View(new HomeViewModel { Places = places });
        }

        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await userService.Logout();
            return RedirectToAction(nameof(LoginController.LoginController.Login), "Login");
        }
    }
}
