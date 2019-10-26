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

        public ProfileController(UserManager<User> userManager, IPetService petService)
        {
            this.userManager = userManager;
            this.petService = petService;
        }

        [HttpGet("/profile")]
        public async Task<IActionResult> ProfileInfo()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var pets = await petService.MyPetsAsync(currentUser);
            return View(new ProfileViewModel {User = currentUser, Pets = pets});
        }
    }
}