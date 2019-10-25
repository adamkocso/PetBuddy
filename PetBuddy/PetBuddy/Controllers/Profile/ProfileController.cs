using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetBuddy.Models;

namespace PetBuddy.Controllers.Profile
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> userManager;

        public ProfileController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet("/profile")]
        public async Task<IActionResult> ProfileInfo()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            return View(currentUser);
        }
    }
}