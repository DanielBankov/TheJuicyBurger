using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;
using JuicyBurger.Services.Models.User;
using System;
using System.Collections.Generic;

namespace JuicyBurger.Services.Models.Receipts
{
    public class ReceiptServiceModel : IMapFrom<Receipt>
    {
        public string Id { get; set; }

        public DateTime IssuedOn;

        public string RecipientId { get; set; }

        public JBUserServiceModel Recipient { get; set; }

        public ICollection<OrderServiceModel> Orders { get; set; }
    }
}
