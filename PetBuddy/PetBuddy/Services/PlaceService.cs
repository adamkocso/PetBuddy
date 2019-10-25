using System.Threading.Tasks;
using PetBuddy.ViewModels;
using Microsoft.EntityFrameworkCore;
using PetBuddy.Models;

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

        public async Task AddPlaceAsync(PlaceInfoViewModel newPlace, User user)
        {
           
            var city = newPlace.City;
            var description = newPlace.Description;
            var price = newPlace.Price;

            var place = await applicationContext.Places.AddAsync(new Place { City = city, Price = price, Description = description, PlaceUri = null, UserId = user.Id });
            await applicationContext.SaveChangesAsync();
            user.PlaceId = place.Entity.PlaceId;
            await applicationContext.SaveChangesAsync();

        }

        public async Task<Place> FindPlaceByIdAsync(long placeId)
        {
            var foundPlace = await applicationContext.Places.Include(p => p.Animals).Include(p => p.Reviews)
                .SingleOrDefaultAsync(x => x.PlaceId == placeId);

            return foundPlace;
        }
    }
}