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
        public DbSet<Receipt> Recipts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
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
            builder.Entity<ProductIngredient>().HasKey(key => new { key.ProductId, key.IngredientId });

            builder.Entity<ProductIngredient>()
                .HasOne<Product>(sc => sc.Product)
                .WithMany(s => s.ProductIngredients)
                .HasForeignKey(sc => sc.ProductId);

            builder.Entity<ProductIngredient>()
                .HasOne<Ingredient>(sc => sc.Ingredient)
                .WithMany(s => s.ProductIngredients)
                .HasForeignKey(sc => sc.IngredientId);

            base.OnModelCreating(builder);
        }
    }
}
