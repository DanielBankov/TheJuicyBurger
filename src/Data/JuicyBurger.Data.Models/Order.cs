using System;
using System.Collections.Generic;

namespace JuicyBurger.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
        }

        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public int Quantity { get; set; }

        public int OrderStatusId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public JBUser Issuer { get; set; }

        public JBUser Dasher { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
