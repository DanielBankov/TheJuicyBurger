using JuicyBurger.Data.Models;
using JuicyBurger.Services.Models.Orders;
using System.Linq;

namespace JuicyBurger.Services.Orders
{
    public interface IOrdersService
    {
        bool CompleteOrder(string orderId);

        bool Create(string id, string comment, string issuer);

        IQueryable<OrderServiceModel> GetAll();

        void SetOrdersToReceipt(Receipt receipt);
    }
}
