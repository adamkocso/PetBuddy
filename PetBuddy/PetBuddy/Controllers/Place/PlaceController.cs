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
                var placeId = await placeService.AddPlaceAsync(newPlace, currentUser);

                if (newPlace.File != null)
                {
                    var errors = imageService.Validate(newPlace.File, newPlace);
                    if (errors.Count != 0)
                    {
                        return View(newPlace);
                    }
                    await imageService.UploadAsync(newPlace.File, placeId, "place");
                    await placeService.SetIndexImageAsync(placeId, "place");
                }

                return RedirectToAction(nameof(PlaceController.PlaceInfo), "Place");
            }

            return View(newPlace);
        }

        [HttpGet("/edit/{placeId}")]
        public async Task<IActionResult> Edit(long placeId)
        {
            var place = await placeService.FindPlaceByIdAsync(placeId);
            return View(new PlaceInfoViewModel{ City = place.City, Description = place.Description, PlaceUri = place.PlaceUri });
        }

        [HttpPost("/edit/{placeId}")]
        public async Task<IActionResult> Edit(PlaceInfoViewModel editPlace, long placeId)
        {
            if (ModelState.IsValid)
            { 
                if (editPlace.File != null)
                {
                    var errors = imageService.Validate(editPlace.File, editPlace);
                    if (errors.Count != 0)
                    {
                        return View(editPlace);
                    }
                    await placeService.EditPlaceAsync(placeId, editPlace);
                    await imageService.UploadAsync(editPlace.File, placeId, "place");
                    await placeService.SetIndexImageAsync(placeId, "place");
                }
                return RedirectToAction(nameof(PlaceController.PlaceInfo), "Place");
            }
           
            return View(editPlace);
        }
    }
}