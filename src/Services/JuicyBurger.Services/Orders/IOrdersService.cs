using JuicyBurger.Data.Models;
using JuicyBurger.Services.Models.Orders;
using System.Linq;

namespace JuicyBurger.Services.Orders
{
    public interface IOrdersService
    {
        bool CompleteOrder(string orderId);

        bool Create(OrderServiceModel serviceModel);

        IQueryable<OrderServiceModel> GetAll();

        void SetOrdersToReceipt(Receipt receipt);
    }
}
