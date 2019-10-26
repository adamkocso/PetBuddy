using PetBuddy.ViewModels;
﻿using PetBuddy.Models;
using System.Threading.Tasks;

namespace PetBuddy.Services
{
    public interface IPlaceService
    {
        Task<long> AddPlaceAsync(PlaceInfoViewModel newPlace, User user);
        Task<Place> FindPlaceByIdAsync(long placeId);
        Task EditPlaceAsync(long placeId, PlaceInfoViewModel editPlace);
        Task SetIndexImageAsync(long placeId, string blobContainerName);
    }
}