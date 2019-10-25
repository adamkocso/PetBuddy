using PetBuddy.ViewModels;
using System.Threading.Tasks;

namespace PetBuddy.Services
{
    public interface IPlaceService
    {
        Task AddPlaceAsync(PlaceInfoViewModel newPlace, string userId);
    }
}