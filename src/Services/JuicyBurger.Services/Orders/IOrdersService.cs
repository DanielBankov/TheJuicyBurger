using JuicyBurger.Services.Models.Orders;
using System.Linq;

namespace JuicyBurger.Services.Orders
{
    public interface IOrdersService
    {
        bool Create(string id, string comment, string issuer);

        IQueryable<OrderServiceModel> GetAll();
    }
}
