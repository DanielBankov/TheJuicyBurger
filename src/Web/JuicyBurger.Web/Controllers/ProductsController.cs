using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JuicyBurger.Web.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult BurgersMenu()
        {
            return View();
        }

        public IActionResult FriesMenu()
        {
            return View();
        }

        public IActionResult DrinksMenu()
        {
            return View();
        }
    }
}