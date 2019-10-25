using PetBuddy.ViewModels;
﻿using PetBuddy.Models;
using System.Threading.Tasks;

namespace PetBuddy.Services
{
    public interface IPlaceService
    {
        Task AddPlaceAsync(PlaceInfoViewModel newPlace, User user);
        Task<Place> FindPlaceByIdAsync(long placeId);
        Task EditPlaceAsync(long placeId, PlaceInfoViewModel editPlace);
    }
}