using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Orders;
using JuicyBurger.Services.Receipts;
using JuicyBurger.Web.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JuicyBurger.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly IReceiptService receiptService;

        public OrdersController(IOrdersService ordersService, IReceiptService receiptService)
        {
            this.ordersService = ordersService;
            this.receiptService = receiptService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            List<OrderCartViewModel> orders = await this.ordersService.GetAll()
                .Where(order => order.OrderStatus.Name == ServicesGlobalConstants.OrderStatusActive
                             && order.IssuerId == this.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .To<OrderCartViewModel>()
                .ToListAsync();

            return this.View(orders);
        }

        [Authorize]
        public async Task<IActionResult> Complete()
        {
            string recipientId = await Task.Run(() => this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            string receiptId = await this.receiptService.Create(recipientId);

            return this.Redirect(ServicesGlobalConstants.RedirectReceiptsDetails + receiptId);
        }
    }
}