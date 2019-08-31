using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;

namespace JuicyBurger.Web.ViewModels.Receipt
{
    public class ReceiptDetailsOrderViewModel : IMapFrom<OrderServiceModel>
    {
        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

    }
}
