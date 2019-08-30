using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Restaurants;
using JuicyBurger.Services.Restaurants;
using JuicyBurger.Web.InputModels.Products;
using JuicyBurger.Web.InputModels.Restaurants;
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
            var contractorId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

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

        [HttpGet("/Administration/Restaurants/Contracts/Create/{id}")]
        public IActionResult Create()
        {
            return View("Contracts/Create");
        }

        [HttpPost("/Administration/Restaurants/Contracts/Create/{id}")]
        public IActionResult Create(RestaurantContractCreateInputModel inputModel, string id)
        {
            //var restaurantRequest = this.restaurantServices.AllNotDeletedRequests().Where(rest => rest.Id == id);

            var restaurantContract = AutoMapper.Mapper.Map<RestaurantContractServiceModel>(inputModel);
            restaurantContract.RestaurantId = id;
            this.restaurantServices.CreateContract(restaurantContract);

            return Redirect("/"); //Redirect to contracts All
        }
    }
}