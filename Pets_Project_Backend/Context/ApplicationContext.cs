using Microsoft.EntityFrameworkCore;
using Pets_Project_Backend.Data.Models.UserModels;

namespace Pets_Project_Backend.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        //Dbsets
        public DbSet<User> Users { get; set; }

        //model config
        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           //default setup for user entity
            modelBuilder.Entity<User>()
                .Property(x => x.Role)
                .HasDefaultValue("User");
            modelBuilder.Entity<User>()
              .Property(i => i.isBlocked)
              .HasDefaultValue(false);
        }

    }
}
