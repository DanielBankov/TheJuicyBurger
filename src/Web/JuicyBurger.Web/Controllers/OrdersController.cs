using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;
using JuicyBurger.Services.Orders;
using JuicyBurger.Services.Receipts;
using JuicyBurger.Web.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult Cart()
        {
            List<OrderCartViewModel> orders = this.ordersService.GetAll()
                .Where(order => order.OrderStatus.Name == "Active"
                             && order.IssuerId == this.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .To<OrderCartViewModel>()
                .ToList();

            return View(orders);
        }

        [HttpPost]
        public IActionResult Complete()
        {
            string recipientId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            string receiptId = this.receiptService.Create(recipientId);

            return this.Redirect($"/Receipts/Details/{receiptId}");
        }
    }
}