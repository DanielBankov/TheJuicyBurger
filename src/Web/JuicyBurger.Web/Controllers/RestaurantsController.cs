using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuicyBurger.Services.Models.Restaurants;
using JuicyBurger.Services.Restaurants;
using JuicyBurger.Web.InputModels.Restaurants;
using Microsoft.AspNetCore.Mvc;

namespace JuicyBurger.Web.Controllers
{
    public class RestaurantsController : Controller
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RestaurantsCreateInputModel inputModel)
        {
            var restaurant = AutoMapper.Mapper.Map<RestaurantsServiceModel>(inputModel);
            this.restaurantServices.Create(restaurant);

            return this.Redirect("/");
        }
    }
}