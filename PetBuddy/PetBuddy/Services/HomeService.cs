using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetBuddy.Models;

namespace PetBuddy.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationContext applicationContext;

        public HomeService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public List<Place> FindAllPlacesAsync()
        { 
            var allPlaces = applicationContext.Places.AsQueryable().OrderBy(p => p.AverageRating).ToList();
            return allPlaces;
        }
    }
}