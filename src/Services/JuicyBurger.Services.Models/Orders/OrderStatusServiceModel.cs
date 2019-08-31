using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;

namespace JuicyBurger.Services.Models.Orders
{
    public class OrderStatusServiceModel : IMapFrom<OrderStatus>, IMapTo<OrderStatus>
    {
        public string Name { get; set; }
    }
}
