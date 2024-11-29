using Microsoft.EntityFrameworkCore;
using Pets_Project_Backend.Data.Models.CategoryModel;
using Pets_Project_Backend.Data.Models.ProductModel;
using Pets_Project_Backend.Data.Models.UserModels;

namespace Pets_Project_Backend.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        //Dbsets
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

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
            //default setup for Product
            modelBuilder.Entity<Product>()
                .Property(pr => pr.ProductPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Product>()
                .Property(pr => pr.StockId)
                .HasDefaultValue(50);

            //relation Product with category (one - many)
            modelBuilder.Entity<Product>()
                .HasOne(a => a._Category)
                .WithMany(b => b._Products)
                .HasForeignKey(c => c.CategoryId);

        }

    }
}
