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

namespace PetBuddy.Controllers.Place
{
    public class PlaceController : Controller
    {
        private readonly IPlaceService placeService;
        private readonly IImageService imageService;
        private readonly UserManager<User> userManager;

        public PlaceController(IPlaceService placeService, IImageService imageService, UserManager<User> userManager)
        {
            this.placeService = placeService;
            this.imageService = imageService;
            this.userManager = userManager;
        }

        [HttpGet("/addplace")]
        public IActionResult Add()
        {

            return View(new PlaceInfoViewModel());
        }

        [Authorize(Roles = "Guest, Admin")]
        [HttpPost("/addplace")]
        public async Task<IActionResult> Add(PlaceInfoViewModel newPlace)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                await placeService.AddPlaceAsync(newPlace, currentUser.Id);
                //    if (newPlace.PlaceUri != null)
                //    {
                //        //    var errors = imageService.Validate(newPlace.PlaceUri, newPlace);
                //        //    if (errors.Count != 0)
                //        //    {
                //        //        return View(newPlace);
                //        //    }

                //        //    await imageService.UploadAsync(newPlace.PlaceUri, placeId);
                //        //await placeService.SetIndexImageAsync(placeId);
                //        //}

                return RedirectToAction(nameof(PlaceC), "Place");
            }

            return View(newPlace);
        }
    }
}