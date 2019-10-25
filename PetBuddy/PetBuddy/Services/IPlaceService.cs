using PetBuddy.Models;
using System.Threading.Tasks;

namespace PetBuddy.Services
{
    public interface IPlaceService
    {
        Task<Place> FindPlaceByIdAsync(long placeId);    
    }
}