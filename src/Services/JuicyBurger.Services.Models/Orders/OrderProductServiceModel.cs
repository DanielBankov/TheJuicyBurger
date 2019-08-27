using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;

namespace JuicyBurger.Services.Models.Orders
{
    public class OrderProductServiceModel : IMapTo<OrderProduct>, IMapFrom<OrderProduct>
    {
        public string OrderId { get; set; }

        public OrderServiceModel Order { get; set; }

        public string ProductId { get; set; }

        public ProductServiceModel Product { get; set; }
    }
}
