using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Restaurants;
using JuicyBurger.Web.ViewModels.Restaurants;
using Microsoft.AspNetCore.Mvc;

namespace JuicyBurger.Web.Areas.Administration.Controllers
{
    public class RestaurantsController : AdminController
    {
        private readonly IRestaurantsService restaurantServices;

        public RestaurantsController(IRestaurantsService restaurantServices)
        {
            this.restaurantServices = restaurantServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Requests()
        {
            var restaurantRequests = this.restaurantServices.AllNotDeletedRequests()
                .To<RestaurantsRequestViewModel>()
                .ToList();

            return View(restaurantRequests);
        }

        public IActionResult Delete(string id)
        {
            this.restaurantServices.Delete(id);

            return this.Redirect("/Administration/Restaurants/Requests");
        }
    }
}