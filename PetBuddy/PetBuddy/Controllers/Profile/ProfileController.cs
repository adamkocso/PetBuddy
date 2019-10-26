using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetBuddy.Models;
using PetBuddy.Services;
using PetBuddy.ViewModels;

namespace PetBuddy.Controllers.Profile
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IPetService petService;
        private readonly IUserService userService;

        public ProfileController(UserManager<User> userManager, IPetService petService, IUserService userService)
        {
            this.userManager = userManager;
            this.petService = petService;
            this.userService = userService;
        }

        [HttpGet("/profile")]
        public async Task<IActionResult> MyProfile()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var pets = await petService.MyPetsAsync(currentUser);
            return View("ProfileInfo", new ProfileViewModel {User = currentUser, Pets = pets, UserId = currentUser.Id});
        }

        [HttpGet("/profileinfo/{userId}")]
        public async Task<IActionResult> ProfileInfo(string userId)
        {
            var user = await userService.FindByIdAsync(userId);
            var pets = await petService.MyPetsAsync(user);
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            return View(new ProfileViewModel {User = user, Pets = pets, UserId = currentUser.Id});
        }
    }
}