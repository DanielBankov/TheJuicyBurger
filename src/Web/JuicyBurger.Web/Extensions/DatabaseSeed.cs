using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace JuicyBurger.Web.Extensions
{
    public static class DatabaseSeed
    {
        public static bool CreateRoles(JuicyBurgerDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
                context.Roles.Add(new IdentityRole { Name = "User", NormalizedName = "USER" });
                context.Roles.Add(new IdentityRole { Name = "Dasher", NormalizedName = "DASHER" });

                context.SaveChanges();
                return true;
            }

            return false;
        }

        public static bool CreateOrderStatuses(JuicyBurgerDbContext context)
        {
            if (!context.OrderStatuses.Any())
            {
                context.OrderStatuses.Add(new OrderStatus { Name = "Active" });
                context.OrderStatuses.Add(new OrderStatus { Name = "Processing" });
                context.OrderStatuses.Add(new OrderStatus { Name = "Finished" });

                context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
