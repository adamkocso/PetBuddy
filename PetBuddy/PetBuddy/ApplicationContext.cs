using Microsoft.EntityFrameworkCore;
using PetBuddy.Models;

namespace PetBuddy
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Review> Reviews { get; set; }
        
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}