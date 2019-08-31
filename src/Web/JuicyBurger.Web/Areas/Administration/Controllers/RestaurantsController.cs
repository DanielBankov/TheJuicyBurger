using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Restaurants;
using JuicyBurger.Services.Restaurants;
using JuicyBurger.Web.InputModels.Restaurants;
using JuicyBurger.Web.ViewModels.Restaurants;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        [HttpGet("/Administration/Restaurants/Contracts/Create/{id}")]
        public IActionResult Create()
        {
            return View("Contracts/Create");
        }

        [HttpPost("/Administration/Restaurants/Contracts/Create/{id}")]
        public IActionResult Create(RestaurantContractCreateInputModel inputModel, string id)
        {
            var restaurantContract = AutoMapper.Mapper.Map<RestaurantContractServiceModel>(inputModel);
            restaurantContract.RestaurantId = id;
            this.restaurantServices.CreateContract(restaurantContract);

            return this.View("Restaurants/Requests"); 
        }
    }
}