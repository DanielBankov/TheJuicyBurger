using JuicyBurger.Data.Models;
using JuicyBurger.Services.Models.Orders;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyBurger.Services.Orders
{
    public interface IOrdersService
    {
        Task<bool> CompleteOrder(string orderId);

        Task<bool> Create(OrderServiceModel serviceModel);

        IQueryable<OrderServiceModel> GetAll();

        Task SetOrdersToReceipt(Receipt receipt);
    }
}
