using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;

namespace JuicyBurger.Services.Models.Orders
{
    public class OrderStatusServiceModel : IMapFrom<OrderStatus>
    {
        public string Name { get; set; }

        //TODO:
        //public ICollection<Review> Reviews { get; set; }
        //public ICollection<Order> Orders { get; set; }
    }
}
