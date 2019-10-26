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
    [Authorize]
    public class PlaceController : Controller
    {
        private readonly IPlaceService placeService;
        private readonly IImageService imageService;
        private readonly UserManager<User> userManager;
        private readonly IReviewService reviewService;

        public PlaceController(IPlaceService placeService, IImageService imageService,
            UserManager<User> userManager, IReviewService reviewService)
        {
            this.placeService = placeService;
            this.imageService = imageService;
            this.userManager = userManager;
            this.reviewService = reviewService;
        }

        [HttpGet("/placeinfo/{placeId}")]
        public async Task<IActionResult> PlaceInfo(long placeId)
        {
            var place = await placeService.FindPlaceByIdAsync(placeId);
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            return View(new ReviewViewModel { Place = place, User = currentUser });
        }

        [HttpGet("/myplace")]
        public async Task<IActionResult> MyPlace()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            if (currentUser.PlaceId != 0)
            {
                var place = await placeService.FindPlaceByIdAsync(currentUser.PlaceId);

                return View("PlaceInfo", new ReviewViewModel()
                { Place = place, User = currentUser });
            }

            return View("PlaceInfo", new ReviewViewModel());
        }

        [HttpPost("/review/{placeId}")]
        public async Task<IActionResult> PlaceReview(ReviewViewModel newReview, long placeId)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                await reviewService.AddReviewAsync(newReview, currentUser.Id, placeId);
                return RedirectToAction(nameof(PlaceController.PlaceInfo), "Place", new { placeId });
            }

            return View("PlaceInfo", newReview);
        }

        [HttpGet("/addplace")]
        public IActionResult Add()
        {

            return View(new PlaceInfoViewModel());
        }

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
                    await imageService.UploadAsync(newPlace.File, placeId.ToString(), "place");
                    await placeService.SetIndexImageAsync(placeId, "place");
                }

                return RedirectToAction(nameof(PlaceController.MyPlace), "Place");
            }

            return View(newPlace);
        }

        [HttpGet("/edit/{placeId}")]
        public async Task<IActionResult> Edit(long placeId)
        {
            var place = await placeService.FindPlaceByIdAsync(placeId);

            return View(new PlaceInfoViewModel{ City = place.City, Description = place.Description, PlaceUri = place.PlaceUri, Price = place.Price });
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
                    await imageService.UploadAsync(editPlace.File, placeId.ToString(), "place");
                    await placeService.SetIndexImageAsync(placeId, "place");
                }
                return RedirectToAction(nameof(PlaceController.MyPlace), "Place");
            }
           
            return View(editPlace);
        }
    }
}