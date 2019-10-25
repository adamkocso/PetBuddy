using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetBuddy.Models;

namespace PetBuddy.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly ApplicationContext applicationContext;

        public PlaceService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<Place> FindPlaceByIdAsync(long placeId)
        {
            var foundPlace = await applicationContext.Places.Include(p => p.Animals).Include(p => p.Reviews)
                .SingleOrDefaultAsync(x => x.PlaceId == placeId);

            return foundPlace;
        }
    }
}