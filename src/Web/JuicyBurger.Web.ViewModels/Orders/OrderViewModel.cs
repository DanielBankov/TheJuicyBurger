using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;
using JuicyBurger.Web.ViewModels.Products;
using JuicyBurger.Web.ViewModels.Users;
using System;

namespace JuicyBurger.Web.ViewModels.Orders
{
    public class OrderViewModel : IMapTo<OrderServiceModel>, IMapFrom<OrderServiceModel>
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public int Quantity { get; set; }

        public int OrderStatusId { get; set; }

        public OrderStatusServiceModel OrderStatus { get; set; }

        public string IssuerId { get; set; }

        public JBUserViewModel Issuer { get; set; }

        public string ProductId { get; set; }

        public ProductViewModel Product { get; set; }
    }
}