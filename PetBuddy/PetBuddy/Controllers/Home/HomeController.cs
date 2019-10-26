using System.Threading.Tasks;
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
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public HomeController(IHomeService homeService, IUserService userService, UserManager<User> userManager)
        {
            this.homeService = homeService;
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpGet("/home")]
        public async Task<IActionResult> Home()
        {
            var places = homeService.FindAllPlacesAsync();
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            ViewBag.UserId = currentUser.Id;
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
