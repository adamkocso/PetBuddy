using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using PetBuddy.Models;
using PetBuddy.Services;
using PetBuddy.ViewModels;

namespace PetBuddy.Controllers.Profile
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly IPetService petService;

        public ProfileController(UserManager<User> userManager, IUserService userService, IPetService petService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.petService = petService;
        }

        [HttpGet("/profile")]
        public async Task<IActionResult> ProfileInfo()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var pets = await petService.MyPetsAsync(currentUser);
            return View(new ProfileViewModel {User = currentUser, Pets = pets});
        }

        [Authorize(Roles = "Guest, Admin")]
        [HttpGet("/settings/{userId}")]
        public async Task<IActionResult> EditProfile()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            ViewBag.UserId = currentUser.Id;
            return View(new EditProfileViewModel
            {
                City = currentUser.City,
                Name = currentUser.UserName
            });
        }

        [Authorize(Roles = "Guest, Admin")]
        [HttpPost("/settings/{userId}")]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editProfile, string userId)
        {
            if (ModelState.IsValid)
            {
                await userService.SaveUserSettings(editProfile, userId);
                return RedirectToAction(nameof(ProfileInfo));
            }

            return View(editProfile);
        }
        
    }
}