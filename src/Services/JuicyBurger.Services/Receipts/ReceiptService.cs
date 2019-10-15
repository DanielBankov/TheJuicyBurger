using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Receipts;
using JuicyBurger.Services.Orders;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyBurger.Services.Receipts
{
    public class ReceiptService : IReceiptService
    {
        private readonly JuicyBurgerDbContext context;
        private readonly IOrdersService ordersService;

        public ReceiptService(JuicyBurgerDbContext context, IOrdersService ordersService)
        {
            this.context = context;
            this.ordersService = ordersService;
        }

        public async Task<string> Create(string recipientId)
        {
            Receipt receipt = new Receipt
            {
                IssuedOn = DateTime.UtcNow,
                RecipientId = recipientId,
            };

            await ordersService.SetOrdersToReceipt(receipt);

            foreach (var order in receipt.Orders)
            {
                await this.ordersService.CompleteOrder(order.Id);
            }

            await this.context.Recipts.AddAsync(receipt);
            await this.context.SaveChangesAsync();

            return receipt.Id;
        } 

        public IQueryable<ReceiptServiceModel> GetAll()
        {
            return this.context.Recipts.To<ReceiptServiceModel>();
        }

        public IQueryable<ReceiptServiceModel> GetByRecipientId(string recipientId)
        {
            return this.context.Recipts.Where(receipt => receipt.RecipientId == recipientId).To<ReceiptServiceModel>();
        }
    }
}
