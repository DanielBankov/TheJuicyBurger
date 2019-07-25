using System;

namespace JuicyBurger.Data.Models
{
    public class RestaurantContracts
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public decimal PricePerMonth { get; set; }

        public string RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public string ContractorId { get; set; }

        public User Contractor { get; set; }
    }
}
