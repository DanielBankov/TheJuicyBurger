using System.Collections.Generic;

namespace JuicyBurger.Data.Models
{
    public class Dasher : JBUser
    {
        public string Name { get; set; }

        public int Rating { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
