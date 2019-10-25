using Microsoft.AspNetCore.Mvc;
using PetBuddy.Services;

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
            
            return View();
        }
    }
}