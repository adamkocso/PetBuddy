using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetBuddy.Models;

namespace PetBuddy
{
    public class ApplicationContext : IdentityDbContext<User>
    {
<<<<<<< HEAD
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Review> Reviews { get; set;}
        
=======
        public DbSet<Pet> Pets  { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
>>>>>>> cf0b53d9145499e5a230d39a61627a93a7193ba3
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