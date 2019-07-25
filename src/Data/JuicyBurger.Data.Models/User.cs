using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace JuicyBurger.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Reviews = new HashSet<Review>();
        }

        public string FullName { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
