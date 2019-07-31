using JuicyBurger.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JuicyBurger.Data
{
    public class JuicyBurgerDbContext : IdentityDbContext<JBUser, IdentityRole, string>
    {
        //public DbSet<JBUserRole> JBUserRoles { get; set; }
        public DbSet<Dasher> Dashers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantContract> RestaurantContracts { get; set; }

        public JuicyBurgerDbContext(DbContextOptions<JuicyBurgerDbContext> options) 
            : base(options)
        {
        }

        public JuicyBurgerDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderProduct>().HasKey(key => new { key.OrderId, key.ProductId });

            builder.Entity<ProductIngredient>().HasKey(key => new { key.ProductId, key.IngredientId });

//            builder.Entity<ProductIngredient>()
//.HasOne(e => e.Ingredient)
//.WithMany(e => e.Products)
                

            base.OnModelCreating(builder);
        }
    }
}
