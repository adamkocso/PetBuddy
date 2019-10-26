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

        public async Task<long> AddPlaceAsync(PlaceInfoViewModel newPlace, User user)
        {
            var city = newPlace.City;
            var description = newPlace.Description;
            var price = newPlace.Price;

            var place = await applicationContext.Places.AddAsync(new Place { City = city, Price = price, Description = description, PlaceUri = null, UserId = user.Id });
            await applicationContext.SaveChangesAsync();
            user.PlaceId = place.Entity.PlaceId;
            await applicationContext.SaveChangesAsync();

            return user.PlaceId;
        }

        public async Task EditPlaceAsync(long placeId, PlaceInfoViewModel editPlace)
        {
            var placeEdit = await FindPlaceByIdAsync(placeId);
            if(placeEdit != null)
            {
                placeEdit.Price = editPlace.Price;
                placeEdit.City = editPlace.City;
                placeEdit.Description = editPlace.Description;
                placeEdit.PlaceUri = editPlace.PlaceUri;
                //placeEdit.Animals = editPlace.Animals;
                applicationContext.Places.Update(placeEdit);
                await applicationContext.SaveChangesAsync();
            }
        }
        public async Task<Place> FindPlaceByIdAsync(long placeId)
        {
            var foundPlace = await applicationContext.Places.Include(p => p.Pets).Include(p => p.Reviews)
                .SingleOrDefaultAsync(x => x.PlaceId == placeId);

            return foundPlace;
        }

        public async Task SetIndexImageAsync(long placeId, string blobContainerName)
        {
            var place = await FindPlaceByIdAsync(placeId);
            var pictures = await imageService.ListAsync(placeId.ToString(), blobContainerName);
            place.PlaceUri = pictures[0].Path;
            applicationContext.Places.Update(place);
            await applicationContext.SaveChangesAsync();
        }
    }
}