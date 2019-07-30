using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace JuicyBurger.Data.Models
{
    public class JBUser : IdentityUser
    {
        public JBUser()
        {
            this.Reviews = new HashSet<Review>();
        }

        public string FullName { get; set; }

        //public JBUserRole UserRole { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
