using Microsoft.EntityFrameworkCore;

namespace PetBuddy
{
    public class ApplicationContext : DbContext
    {
        
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}