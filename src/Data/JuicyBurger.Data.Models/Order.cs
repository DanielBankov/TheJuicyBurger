using System;
using System.Collections.Generic;

namespace JuicyBurger.Data.Models
{
    public class Order
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public int Quantity { get; set; }

        public int OrderStatusId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string IssuerId { get; set; }

        public JBUser Issuer { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }
    }
}
