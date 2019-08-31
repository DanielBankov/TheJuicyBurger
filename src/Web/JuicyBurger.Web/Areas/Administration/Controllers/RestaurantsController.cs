using JuicyBurger.Services.GlobalConstants;
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
            return this.View();
        }

        public IActionResult Requests()
        {
            var restaurantRequests = this.restaurantServices.AllNotDeletedRequests()
                .To<RestaurantsRequestViewModel>()
                .ToList();

            return this.View(restaurantRequests);
        }

        public IActionResult Delete(string id)
        {
            this.restaurantServices.Delete(id);

            return this.Redirect(ServicesGlobalConstants.RedirectRestaurantRequest);
        }

        [HttpGet(ServicesGlobalConstants.HttpRestaurantsContractsCreateId)]
        public IActionResult Create()
        {
            return this.View(ServicesGlobalConstants.ViewContractsCreate);
        }

        [HttpPost(ServicesGlobalConstants.HttpRestaurantsContractsCreateId)]
        public IActionResult Create(RestaurantContractCreateInputModel inputModel, string id)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect(ServicesGlobalConstants.HttpRestaurantsContractsCreateId);
            }

            var restaurantContract = AutoMapper.Mapper.Map<RestaurantContractServiceModel>(inputModel);
            restaurantContract.RestaurantId = id;
            this.restaurantServices.CreateContract(restaurantContract);

            return this.View(ServicesGlobalConstants.ViewRestaurantsContracts); 
        }
    }
}