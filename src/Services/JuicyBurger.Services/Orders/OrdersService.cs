using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyBurger.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly int num = ServicesGlobalConstants.ComparisonNumberForResultFromDbSaveChanges;
        private const int orderQuantity = 1;


        private readonly JuicyBurgerDbContext context;

        public OrdersService(JuicyBurgerDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CompleteOrder(string orderId)
        {
            var orderDb = await this.context.Orders.SingleOrDefaultAsync(order => order.Id == orderId);

            if (orderDb == null)
            {
                throw new InvalidOperationException(ServicesGlobalConstants.NoActiveOrdersExceptionMessage);
            }

            orderDb.OrderStatus = await this.context.OrderStatuses
                .SingleOrDefaultAsync(orderStatus => orderStatus.Name == ServicesGlobalConstants.OrderStatusFinish);

            await Task.Run(() => this.context.Orders.Update(orderDb));
            var result = await this.context.SaveChangesAsync();

            return result > num;
        }

        public async Task<bool> Create(OrderServiceModel orderService)
        {
            //IsProductAlreadyOrderedResult to increase quantity
            var IsProductAlreadyOrderedResult = await IsProductAlreadyOrdered(orderService);

            if (IsProductAlreadyOrderedResult)
            {
                return true;
            }

            Order order = orderService.To<Order>();

            var issuedOn = DateTime.UtcNow;
            order.Quantity = orderQuantity;
            order.IssuedOn = DateTime.UtcNow;

            order.OrderStatus = await this.context.OrderStatuses
                .SingleOrDefaultAsync(os => os.Name == ServicesGlobalConstants.OrderStatusActive);

            await this.context.Orders.AddAsync(order);
            var result = await this.context.SaveChangesAsync();

            return result > num;
        }

        public IQueryable<OrderServiceModel> GetAll()
        {
            return this.context.Orders.To<OrderServiceModel>();
        }

        public async Task SetOrdersToReceipt(Receipt receipt)
        {
            var orders = await this.context.Orders
                .Where(order => order.IssuerId == receipt.RecipientId &&
                                order.OrderStatus.Name == ServicesGlobalConstants.OrderStatusActive)
                .ToListAsync();

            receipt.Orders = orders;
        }

        private async Task<bool> IsProductAlreadyOrdered(OrderServiceModel orderService)
        {
            var allOrders = await GetAll().ToListAsync();
            var ordaredOrder = new OrderServiceModel();
            var result = num;

            foreach (var currOrder in allOrders)
            {
                if (currOrder.ProductId == orderService.ProductId)
                {
                    ordaredOrder = currOrder;

                    var orderAlreadyOrdered = await this.context.Orders
                        .SingleOrDefaultAsync(or => or.Id == ordaredOrder.Id);
                    orderAlreadyOrdered.Quantity++;

                    await Task.Run(() => this.context.Orders.Update(orderAlreadyOrdered));
                    result = await this.context.SaveChangesAsync();

                    return result > num;
                }
            }

            return result > num;
        }
    }
}
