using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PetBuddy
{
    public class ApplicationContext : DbContext
    {
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