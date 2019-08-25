using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;
using JuicyBurger.Services.Orders;
using JuicyBurger.Web.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;

namespace JuicyBurger.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService OrdersService)
        {
            this.ordersService = OrdersService;
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
        public IActionResult Cart(string card)
        {
            return View();
        }
    }
}