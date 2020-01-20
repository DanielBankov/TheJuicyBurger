using JuicyBurger.Service.Products;
using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Ingredients;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;
using JuicyBurger.Services.Orders;
using JuicyBurger.Web.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JuicyBurger.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly IIngredientsService ingredientsService;
        private readonly IOrdersService ordersService;

        public ProductsController(IProductsService productsService, IOrdersService OrdersService, IIngredientsService ingredientsService)
        {
            this.productsService = productsService;
            this.ingredientsService = ingredientsService;
            this.ordersService = OrdersService;
        }

        [HttpGet(ServicesGlobalConstants.HttpProductsAllId)]
        public async Task<IActionResult> All(int? id)
        {
            await GetAllProductTypes();

            this.ViewData["currentMenuCategory"] = id == null ? 1 : id;

            var products = await this.productsService.AllByProductTypeId(id)
                .Select(prod => prod.To<ProductViewModel>())
                .ToListAsync();

            return this.View(products);
        }

        [Route(ServicesGlobalConstants.HttpProductsDetailsId)]
        public async Task<IActionResult> Details(string id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.View();
            }

            var serviceModel = await this.productsService.Details(id);
            var viewModel = AutoMapper.Mapper.Map<ProductsDetailsViewModel>(serviceModel);
            var ingNames = await this.productsService.GetAllIngredientsName(serviceModel);

            this.ViewData[ServicesGlobalConstants.IngredientsNameViewData] = ingNames;

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString)
        {
            await GetAllProductTypes();

            var searchedProducts = await this.productsService.Search(searchString)
                .Select(product => product.To<ProductViewModel>())
                .ToListAsync();

            return this.View(ServicesGlobalConstants.ViewProductsAll, searchedProducts);
        }

        [Authorize]
        public async Task<IActionResult> Order( string id) 
        //TODO: add quantity in details view
        {
            var inputModel = new  OrderServiceModel () ;
            OrderServiceModel orderServiceModel = inputModel.To<OrderServiceModel>();

            orderServiceModel.IssuerId = await Task.Run(() => this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            orderServiceModel.ProductId = id;
            await this.ordersService.Create(orderServiceModel);

            return this.RedirectToAction("Cart", "Orders");
        }

        private Task GetAllProductTypes()
        {
            var allProductTypes = this.productsService.GetAllTypes();

            this.ViewData[ServicesGlobalConstants.ProductTypesMenuViewData] = allProductTypes
                .Select(productType => new ProductTypeViewModel
            {
                Id = productType.Id,
                Name = productType.Name
            });

            return Task.CompletedTask;
        }
    }
}