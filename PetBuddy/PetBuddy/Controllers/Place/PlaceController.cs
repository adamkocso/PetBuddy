using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetBuddy.Models;
using PetBuddy.Services;
using PetBuddy.ViewModels;
using Microsoft.AspNetCore.Http;

namespace PetBuddy.Controllers.Place
{
    [AllowAnonymous]
    public class PlaceController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IImageService imageService;
        private readonly IPlaceService placeService;

        public PlaceController(UserManager<User> userManager, IImageService imageService, IPlaceService placeService)
        {
            this.userManager = userManager;
            this.imageService = imageService;
            this.placeService = placeService;
        }

        [AllowAnonymous]
        [HttpGet("/placeInfo")]
        public async Task<IActionResult> PlaceInfo()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            if (currentUser.PlaceId != 0)
            {
                var place = await placeService.FindPlaceByIdAsync(currentUser.PlaceId);

                return View(new PlaceInfoViewModel
                { User = currentUser, Place = place });
            }
            
            return View(new PlaceInfoViewModel
                { User = currentUser });
        }
    }
}
