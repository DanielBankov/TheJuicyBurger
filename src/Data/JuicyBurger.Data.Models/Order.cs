using System;
using System.Collections.Generic;

namespace JuicyBurger.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.Product = new HashSet<Product>();
        }

        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public int Quantity { get; set; }

        public int OrderStatusId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public User Issuer { get; set; }

        public User Dasher { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
