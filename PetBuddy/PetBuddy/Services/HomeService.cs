using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetBuddy.Models;
using PetBuddy.Utils;

namespace PetBuddy.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationContext applicationContext;

        public HomeService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public List<Place> FindAllPlaces()
        { 
            var allPlaces = applicationContext.Places.AsQueryable().OrderBy(p => p.AverageRating).ToList();
            return allPlaces;
        }
        
        public List<String> FindUniqueCities()
        {
            var places = applicationContext.Places.AsQueryable().ToList();
            var listOfCities = new List<String>();
            for (var i = 0; i < places.Count; i++)
            {
                listOfCities.Add(places[i].City);
            }
            var uniqueCities = listOfCities.Distinct().ToList();
            return uniqueCities;
        }

        public async Task<List<Place>> FilterPlacesAsync(QueryParams queryParam)
        {
            var places = await applicationContext.Places
                .Include(p => p.Pets)
                .Include(p => p.Reviews)
                .Where(p => p.City.Contains(queryParam.City) || String.IsNullOrEmpty(queryParam.City))
                .Where(p => p.Price == queryParam.Price || queryParam.Price == 0)
                .OrderBy(p => p.AverageRating).ToListAsync();
            return places;
        }
    }
}