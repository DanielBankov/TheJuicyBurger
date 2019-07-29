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

        public JBUser Issuer { get; set; }

        public JBUser Dasher { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
