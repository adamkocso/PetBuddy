using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetBuddy.Controllers.Profile;
using PetBuddy.Models;
using PetBuddy.Services;
using PetBuddy.Viewmodels;

namespace PetBuddy.Controllers.Pet
{
    public class PetController : Controller
    {
        
        private readonly UserManager<User> userManager;
        private readonly IPetService petService;

        public PetController(UserManager<User> userManager, IPetService petService)
        {
            this.userManager = userManager;
            this.petService = petService;
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
                await petService.AddPetAsync(newPet, currentUser);
                return RedirectToAction(nameof(ProfileController.ProfileInfo), "Profile");
            }

            return View(newPet);
        }
        
        [HttpGet("/deletepet/{petId}")]
        public async Task<IActionResult> DeletePet(long petId)
        {
            await petService.DeletePetAsync(petId);
            return RedirectToAction(nameof(ProfileController.ProfileInfo), "Profile");
        }

        [HttpGet("/edit/{petId}")]
        public async Task<IActionResult> EditPet(long petId)
        {
            var pet = await petService.FindPetByIdAsync(petId);
            var editpet = new PetViewModel
            {
                PetName = pet.PetName,
                PetType = pet.PetType,
                PetDescription = pet.PetDescription
            };
            return View(editpet);
        }

        [HttpPost("/edit/{petId}")]
        public async Task<IActionResult> EditPet(PetViewModel editPet, long petId)
        {
            if (ModelState.IsValid)
            {
                await petService.EditPetAsync(editPet, petId);
                return RedirectToAction(nameof(ProfileController.ProfileInfo), "Profile");
            }
            
            return View(editPet);
        }
    }
}