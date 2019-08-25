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

            order.OrderStatus = context.OrderStatuses.FirstOrDefault(os => os.Name == "Active");

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
    }
}
