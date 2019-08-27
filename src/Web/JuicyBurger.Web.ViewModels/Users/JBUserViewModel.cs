using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.User;
using JuicyBurger.Web.ViewModels.Orders;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace JuicyBurger.Web.ViewModels.Users
{
    public class JBUserViewModel : IdentityUser, IMapFrom<JBUserServiceModel>
    {
        public string FullName { get; set; }

        public List<OrderViewModel> Orders { get; set; }
    }
}
