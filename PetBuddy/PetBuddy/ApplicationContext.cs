using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetBuddy.Models;

namespace PetBuddy
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() },
                new IdentityRole { Name = "Guest", NormalizedName = "Guest".ToUpper() }
            );
        }
    }
}