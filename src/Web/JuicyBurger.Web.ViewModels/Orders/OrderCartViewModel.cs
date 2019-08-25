using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;

namespace JuicyBurger.Web.ViewModels.Orders
{
    public class OrderCartViewModel : IMapFrom<OrderServiceModel>
    {
        public string Id { get; set; }

        public string ProductImage { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal ProductPrice { get; set; }
    }
}
