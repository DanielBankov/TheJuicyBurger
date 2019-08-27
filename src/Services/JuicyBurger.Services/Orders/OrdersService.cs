using System;
using System.Collections.Generic;
using System.Linq;
using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;

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
            //var productDb = context.Products.Where(product => product.Id == id);

            //var order = new OrderProduct
            //{
            //      ProductId = id
            //};

            var issuedOn = DateTime.UtcNow;

            var order = new Order
            {
                Quantity = 1,
                IssuedOn = issuedOn,
                IssuerId = issuer,
                ProductId = id
            };

            order.OrderStatus = context.OrderStatuses.SingleOrDefault(os => os.Name == "Active");

            //order.OrderProducts.Add(new OrderProduct
            //{
            //    ProductId = id,
            //    Order = order
            //});

            context.Orders.Add(order);
            var result = context.SaveChanges();

            return result > 0;
        }

        public IQueryable<OrderServiceModel> GetAll()
        {
            //var userDb = context.Users.SingleOrDefault(user => user.Id == userId); // include


            var a = this.context.Orders.To<OrderServiceModel>();
            return a;
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
