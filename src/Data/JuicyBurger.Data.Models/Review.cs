using System.Collections.Generic;

namespace JuicyBurger.Data.Models
{
    public class Review
    {
        public Review()
        {
            this.Users = new HashSet<JBUser>();
        }

        public string Id { get; set; }

        public int Rating { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public ICollection<JBUser> Users { get; set; }
    }
}
