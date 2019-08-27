using System;
using System.Collections.Generic;

namespace JuicyBurger.Data.Models
{
    public class Receipt
    {
        public Receipt()
        {
            this.Orders = new List<Order>();
        }

        public string Id { get; set; }
        
        public DateTime IssuedOn { get; set; }

        public string RecipientId { get; set; }

        public JBUser Recipient { get; set; }

        public List<Order> Orders { get; set; }
    }
}
