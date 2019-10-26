using System.Threading.Tasks;
using PetBuddy.Models;
using PetBuddy.ViewModels;

namespace PetBuddy.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationContext applicationContext;
        public ReviewService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task AddReviewAsync(ReviewViewModel newReview, string userId, long placeId)
        {
            var review = new Review
            {
                Rating = newReview.Rating,
                Text = newReview.Text,
                UserId = userId,
                PlaceId = placeId
            };
            await applicationContext.Reviews.AddAsync(review);
            await applicationContext.SaveChangesAsync();
        }
    }
}