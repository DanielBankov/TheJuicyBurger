using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;
using System;
using System.Linq;

namespace JuicyBurger.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly JuicyBurgerDbContext context;

        public OrdersService(JuicyBurgerDbContext context)
        {
            this.context = context;
        }

        public bool CompleteOrder(string orderId)
        {
            var orderDb = this.context.Orders.SingleOrDefault(order => order.Id == orderId);

            if (orderDb == null)
            {
                throw new InvalidOperationException("No active orders!");
                // try catch in controller and redirect to error page
            }

            orderDb.OrderStatus = this.context.OrderStatuses.SingleOrDefault(os => os.Name == "Finished");

            this.context.Orders.Update(orderDb);
            var result = this.context.SaveChanges();

            return result > 0;
        }

        public bool Create(string id, string comment, string issuer)
        {
            //TODO: add comment 

            var issuedOn = DateTime.UtcNow;

            var order = new Order
            {
                Quantity = 1,
                IssuedOn = issuedOn,
                IssuerId = issuer,
                ProductId = id
            };

            order.OrderStatus = context.OrderStatuses.SingleOrDefault(os => os.Name == "Active");

            context.Orders.Add(order);
            var result = context.SaveChanges();

            return result > 0;
        }

        public IQueryable<OrderServiceModel> GetAll()
        {
            return this.context.Orders.To<OrderServiceModel>();
        }

        public void SetOrdersToReceipt(Receipt receipt)
        {
            var orders = context.Orders
                .Where(order => order.IssuerId == receipt.RecipientId && order.OrderStatus.Name == "Active")
                .ToList();

            receipt.Orders = orders;
        }
    }
}
