using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.GlobalConstants;
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
                context.Roles.Add(new IdentityRole
                { Name = ServicesGlobalConstants.Admin, NormalizedName = ServicesGlobalConstants.AdminUpperCase });
                context.Roles.Add(new IdentityRole
                { Name = ServicesGlobalConstants.User, NormalizedName = ServicesGlobalConstants.UserUpperCase });
                //context.Roles.Add(new IdentityRole { Name = "Dasher", NormalizedName = "DASHER" });

                context.SaveChanges();
                return true;
            }

            return false;
        }

        public static bool CreateOrderStatuses(JuicyBurgerDbContext context)
        {
            if (!context.OrderStatuses.Any())
            {
                context.OrderStatuses.Add(new OrderStatus { Name = ServicesGlobalConstants.OrderStatusActive });
                context.OrderStatuses.Add(new OrderStatus { Name = ServicesGlobalConstants.OrderStatusFinish });
                //context.OrderStatuses.Add(new OrderStatus { Name = "Processing" });

                context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
