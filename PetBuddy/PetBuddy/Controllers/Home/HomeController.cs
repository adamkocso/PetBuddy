using Microsoft.AspNetCore.Mvc;
using PetBuddy.Services;
using PetBuddy.ViewModels;

namespace PetBuddy.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        [HttpGet("/")]
        public IActionResult Home()
        {
            var places = homeService.FindAllPlacesAsync();
            return View(new HomeViewModel {Places =  places} );
        }
    }
}