using System.Threading.Tasks;
using PetBuddy.ViewModels;

namespace PetBuddy.Services
{
    public interface IReviewService
    {
        Task AddReviewAsync(ReviewViewModel newReview, string userId, long placeId);
    }
}