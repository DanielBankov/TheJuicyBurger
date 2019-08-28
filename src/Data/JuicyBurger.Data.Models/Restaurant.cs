using System;

namespace JuicyBurger.Data.Models
{
    public class Restaurant
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Company { get; set; }

        public DateTime IssuedOn { get; set; }

        public string Location { get; set; }

        public string PhoneNumber { get; set; }

        public string VatNumber { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
