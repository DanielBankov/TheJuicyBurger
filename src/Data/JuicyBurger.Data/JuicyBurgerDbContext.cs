using JuicyBurger.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace JuicyBurger.Data
{
    public class JuicyBurgerDbContext : IdentityDbContext<User, UserRole, string>
    {
    }
}
