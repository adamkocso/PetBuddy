namespace PetBuddy.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationContext applicationContext;
        public ReviewService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
    }
}