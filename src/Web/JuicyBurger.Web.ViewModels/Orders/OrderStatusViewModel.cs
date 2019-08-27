using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;

namespace JuicyBurger.Web.ViewModels.Orders
{
    public class OrderStatusViewModel : IMapFrom<OrderStatusServiceModel>
    {
        public string Name { get; set; }
    }
}
