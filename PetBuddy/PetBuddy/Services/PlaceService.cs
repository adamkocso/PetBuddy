namespace PetBuddy.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly ApplicationContext applicationContext;

        public PlaceService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
    }
}