using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetBuddy.Controllers.Profile;
using PetBuddy.Models;
using PetBuddy.Services;
using PetBuddy.ViewModels;

namespace PetBuddy.Controllers.Pet
{
    public class PetController : Controller
    {
        
        private readonly UserManager<User> userManager;
        private readonly IPetService petService;
        private readonly IImageService imageService;
        private readonly IPlaceService placeService;

        public PetController(UserManager<User> userManager, IPetService petService, 
            IImageService imageService, IPlaceService placeService)
        {
            this.userManager = userManager;
            this.petService = petService;
            this.imageService = imageService;
            this.placeService = placeService;
        }

        [HttpGet("/newpet")]
        public IActionResult NewPet()
        {
            return View(new PetViewModel());
        }

        [HttpPost("/newpet")]
        public async Task<IActionResult> NewPet(PetViewModel newPet)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                var petId = await petService.AddPetAsync(newPet, currentUser);

                if (newPet.File != null)
                {
                    await imageService.UploadAsync(newPet.File, petId, "pet");
                    await petService.SetIndexImageAsync(petId, "pet");
                }

                return RedirectToAction(nameof(ProfileController.MyProfile), "Profile");
            }

            return View(newPet);
        }
        
        [HttpGet("/deletepet/{petId}")]
        public async Task<IActionResult> DeletePet(long petId)
        {
            await petService.DeletePetAsync(petId);
            return RedirectToAction(nameof(ProfileController.MyProfile), "Profile");
        }

        [HttpGet("/editpet/{petId}")]
        public async Task<IActionResult> EditPet(long petId)
        {
            var pet = await petService.FindPetByIdAsync(petId);
            return View(new PetViewModel {PetName = pet.PetName, PetType = pet.PetType, PetDescription = pet.PetDescription});
        }

        [HttpPost("/editpet/{petId}")]
        public async Task<IActionResult> EditPet(PetViewModel editPet, long petId)
        {
            if (ModelState.IsValid)
            {
                await petService.EditPetAsync(editPet, petId);
                if (editPet.File != null)
                {
                    await imageService.UploadAsync(editPet.File, petId, "pet");
                    await petService.SetIndexImageAsync(petId, "pet");
                }
                return RedirectToAction(nameof(ProfileController.MyProfile), "Profile");
            }
            
            return View(editPet);
        }
    }
}