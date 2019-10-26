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
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly IPetService petService;
        private readonly IImageService imageService;

        public ProfileController(UserManager<User> userManager, IUserService userService,
            IPetService petService, IImageService imageService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.petService = petService;
            this.imageService = imageService;
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
                if (editProfile.File != null)
                {
                    await userService.SaveUserSettings(editProfile, userId);
                    await imageService.UploadAsync(editProfile.File, userId, "user");
                    await userService.SetIndexImageAsync(userId, "user");
                }
                return RedirectToAction(nameof(ProfileInfo));
            }

            return View(editProfile);
        }
    }
}