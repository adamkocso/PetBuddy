using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetBuddy.Models;
using PetBuddy.Services;
using PetBuddy.ViewModels;

namespace PetBuddy.Controllers.HomeController
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly UserManager<User> userManager;

        public HomeController(IHomeService homeService, UserManager<User> userManager)
        {
            this.homeService = homeService;
            this.userManager = userManager;
        }

        [HttpGet("/")]
        public IActionResult Home()
        {
            var places = homeService.FindAllPlacesAsync();
            return View(new HomeViewModel { Places = places });
        }
    }
}
