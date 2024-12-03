using Microsoft.EntityFrameworkCore;
using Pets_Project_Backend.Data.Models.CartModel;
using Pets_Project_Backend.Data.Models.CategoryModel;
using Pets_Project_Backend.Data.Models.ProductModel;
using Pets_Project_Backend.Data.Models.UserModels;
using Pets_Project_Backend.Data.Models.WhishListModel;

namespace Pets_Project_Backend.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        //Dbsets
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WhishList> WhishList { get; set; }

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

            //default setup for qty in cartItem
            modelBuilder.Entity<CartItem>()
                .Property(pr => pr.ProductQty)
                .HasDefaultValue(1);

            //---------------------------------

            //relation Product with category (one - many)
            modelBuilder.Entity<Product>()
                .HasOne(a => a._Category)
                .WithMany(b => b._Products)
                .HasForeignKey(c => c.CategoryId);

            //user and cart
            modelBuilder.Entity<Cart>()
                .HasOne(a => a._User)
                .WithOne(b => b._Cart)
                .HasForeignKey<Cart>(c => c.UserId);

            //cart and cart itens
            modelBuilder.Entity<CartItem>()
                .HasOne(a=>a._Cart)
                .WithMany(b=>b._Items)
                .HasForeignKey(c=>c.CartId);

            //cart items and product
            modelBuilder.Entity<CartItem>()
                .HasOne(a=>a._Product)
                .WithMany(b=>b._CartItems)
                .HasForeignKey(c=>c.ProductId);

            //user and whishlist
            modelBuilder.Entity<WhishList>()
                .HasOne(a=>a._User)
                .WithMany(b=>b._WhishLists)
                .HasForeignKey(c=>c.userId);

            //wishlist with product
            modelBuilder.Entity<WhishList>()
                .HasOne(a=>a._Product)
                .WithMany()  //Products does not have a navigation property to WhishList.
                .HasForeignKey(c=>c.productId);  
                

        }

    }
}
