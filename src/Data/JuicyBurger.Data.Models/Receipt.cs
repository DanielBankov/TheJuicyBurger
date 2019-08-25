using System;
using System.Collections.Generic;

namespace JuicyBurger.Data.Models
{
    public class Receipt
    {
        public DateTime IssuedOn;

        public string RecipientId { get; set; }

        public JBUser Recipient { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
