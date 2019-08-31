using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;

namespace JuicyBurger.Web.ViewModels.Products
{
    public class ProductOrderInputModel : IMapFrom<OrderServiceModel>, IMapTo<OrderServiceModel>
    {
        public string ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
