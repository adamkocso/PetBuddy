namespace PetBuddy.Services
{
    public class PetService : IPetService
    {
        private readonly ApplicationContext applicationContext;

        public PetService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
    }
}