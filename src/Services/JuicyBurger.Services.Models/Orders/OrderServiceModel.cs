using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;
using JuicyBurger.Services.Models.User;
using System;

namespace JuicyBurger.Services.Models.Orders
{
    public class OrderServiceModel : /*IMapTo<Order>,*/ IMapFrom<Order>
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public int Quantity { get; set; }

        public int OrderStatusId { get; set; }

        public OrderStatusServiceModel OrderStatus { get; set; }

        public string IssuerId { get; set; }

        public JBUserServiceModel Issuer { get; set; }

        public string ProductId { get; set; }

        public ProductServiceModel Product { get; set; }
    }
}
