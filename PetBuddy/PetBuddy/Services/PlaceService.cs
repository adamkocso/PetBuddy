using System.Threading.Tasks;
using PetBuddy.ViewModels;

namespace PetBuddy.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IImageService imageService;

        public PlaceService(ApplicationContext applicationContext, IImageService imageService)
        {
            this.applicationContext = applicationContext;
            this.imageService = imageService;
        }

        public async Task AddPlaceAsync(PlaceInfoViewModel newPlace, string userId)
        {
            var city = newPlace.City;
            var description = newPlace.Description;
            var price = newPlace.Price;

            await applicationContext.Places.AddAsync(new Models.Place { City = city, Price = price, Description = description, PlaceUri = null, UserId = userId });
            await applicationContext.SaveChangesAsync();
        }
    }
}