using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PetBuddy.Models;
using PetBuddy.Utils;

namespace PetBuddy.Services
{
    public interface IHomeService
    {
        List<Place> FindAllPlaces();
        List<String> FindUniqueCities();
        Task<List<Place>> FilterPlacesAsync(QueryParams queryParam);
    }
}