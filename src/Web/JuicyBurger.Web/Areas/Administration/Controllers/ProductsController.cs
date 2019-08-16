using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyBurger.Web.Areas.Administration.Controllers
{
    public class ProductsController : AdminController
    {
        public IActionResult All()
        {
            return this.View();
        }
    }
}
