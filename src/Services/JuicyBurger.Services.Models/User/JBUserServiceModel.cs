using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace JuicyBurger.Services.Models.User
{
    public class JBUserServiceModel : IdentityUser, IMapFrom<JBUser>, IMapTo<JBUser>
    {
        public string FullName { get; set; }

        public List<OrderServiceModel> Orders { get; set; }
    }
}
