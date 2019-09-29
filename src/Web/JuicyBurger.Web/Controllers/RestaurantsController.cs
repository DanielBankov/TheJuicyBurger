﻿using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Models.Restaurants;
using JuicyBurger.Services.Restaurants;
using JuicyBurger.Web.InputModels.Restaurants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            return this.View();
        }

        [HttpGet]
        public IActionResult CreateRequest()
        {
            return this.View();
        }

        //Create partner request
        [Authorize]
        [HttpPost]
        public IActionResult CreateRequest(RestaurantsCreateInputModel inputModel)
        {
            var contractorId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var restaurant = AutoMapper.Mapper.Map<RestaurantsServiceModel>(inputModel);
            this.restaurantServices.CreatePartnerRequest(restaurant, contractorId);

            return this.Redirect(ServicesGlobalConstants.HomeIndex);
        }
    }
}