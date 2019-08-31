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
            }

            orderDb.OrderStatus = this.context.OrderStatuses.SingleOrDefault(os => os.Name == "Finished");

            this.context.Orders.Update(orderDb);
            var result = this.context.SaveChanges();

            return result > 0;
        }

        public bool Create(OrderServiceModel orderService)
        {
            //IsProductAlreadyOrderedResult to increase quantity
            var IsProductAlreadyOrderedResult = IsProductAlreadyOrdered(orderService);

            if (IsProductAlreadyOrderedResult)
            {
                return true;
            }

            Order order = orderService.To<Order>();

            var issuedOn = DateTime.UtcNow;
            order.Quantity = 1; ////////// CONST
            order.IssuedOn = DateTime.UtcNow;

            order.OrderStatus = this.context.OrderStatuses.SingleOrDefault(os => os.Name == "Active");

            this.context.Orders.Add(order);
            var result = context.SaveChanges();

            return result > 0;
        }

        public IQueryable<OrderServiceModel> GetAll()
        {
            return this.context.Orders.To<OrderServiceModel>();
        }

        public void SetOrdersToReceipt(Receipt receipt)
        {
            var orders = this.context.Orders
                .Where(order => order.IssuerId == receipt.RecipientId && order.OrderStatus.Name == "Active")
                .ToList();

            receipt.Orders = orders;
        }

        private bool IsProductAlreadyOrdered(OrderServiceModel orderService)
        {
            var allOrders = GetAll().ToList();
            var ordaredOrder = new OrderServiceModel();
            var result = 0;

            foreach (var currOrder in allOrders)
            {
                if (currOrder.ProductId == orderService.ProductId)
                {
                    ordaredOrder = currOrder;

                    var orderAlreadyOrdered = context.Orders.SingleOrDefault(or => or.Id == ordaredOrder.Id);
                    orderAlreadyOrdered.Quantity++;
                    this.context.Orders.Update(orderAlreadyOrdered);
                    result = context.SaveChanges();

                    return result > 0;
                }
            }

            return result > 0;
        }
    }
}
