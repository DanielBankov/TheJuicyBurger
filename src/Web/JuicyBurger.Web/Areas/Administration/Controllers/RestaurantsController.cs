using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Restaurants;
using JuicyBurger.Services.Restaurants;
using JuicyBurger.Web.InputModels.Restaurants;
using JuicyBurger.Web.ViewModels.Restaurants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyBurger.Web.Areas.Administration.Controllers
{
    public class RestaurantsController : AdminController
    {
        private readonly IRestaurantsService restaurantServices;

        public RestaurantsController(IRestaurantsService restaurantServices)
        {
            this.restaurantServices = restaurantServices;
        }

        public async Task<IActionResult> Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Requests()
        {
            var restaurantRequests = await this.restaurantServices.AllNotDeletedRequests();
            var restaurantRequestsView = restaurantRequests.To<RestaurantsRequestViewModel>().ToList();

            return this.View(restaurantRequestsView);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.restaurantServices.Delete(id);

            return this.Redirect(ServicesGlobalConstants.RedirectRestaurantRequest);
        }

        [HttpGet(ServicesGlobalConstants.HttpRestaurantsContractsCreateId)]
        public async Task<IActionResult> Create()
        {
            return this.View(ServicesGlobalConstants.ViewContractsCreate);
        }

        [HttpPost(ServicesGlobalConstants.HttpRestaurantsContractsCreateId)]
        public async Task<IActionResult> Create(RestaurantContractCreateInputModel inputModel, string id)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect(ServicesGlobalConstants.HttpRestaurantsContractsCreateId);
            }

            var restaurantContract = AutoMapper.Mapper.Map<RestaurantContractServiceModel>(inputModel);
            restaurantContract.RestaurantId = id;
            await this.restaurantServices.CreateContract(restaurantContract);

            return this.Redirect(ServicesGlobalConstants.HomeIndex); 
        }
    }
}