using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Models.Restaurants;
using JuicyBurger.Services.Restaurants;
using JuicyBurger.Web.InputModels.Restaurants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JuicyBurger.Web.Controllers
{
    public class RestaurantsController : Controller
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

        [HttpGet]
        public async Task<IActionResult> CreateRequest()
        {
            return this.View();
        }

        //Create partner request
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRequest(RestaurantsCreateInputModel inputModel)
        {
            var contractorId = await Task.Run(() => this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var restaurant = AutoMapper.Mapper.Map<RestaurantsServiceModel>(inputModel);
            await this.restaurantServices.CreatePartnerRequest(restaurant, contractorId);

            return this.Redirect(ServicesGlobalConstants.HomeIndex);
        }
    }
}