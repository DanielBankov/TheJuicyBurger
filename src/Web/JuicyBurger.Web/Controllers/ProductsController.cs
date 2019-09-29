using JuicyBurger.Service.Products;
using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Ingredients;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Orders;
using JuicyBurger.Services.Orders;
using JuicyBurger.Web.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

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
        public IActionResult All(int id)
        {
            GetAllProductTypes();

            var products = this.productsService.AllByProductTypeId(id)
                .Select(prod => prod.To<ProductViewModel>())
                .ToList();

            return this.View(products);
        }

        [Route(ServicesGlobalConstants.HttpProductsDetailsId)]
        public IActionResult Details(string id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.View();
            }

            var serviceModel = this.productsService.Details(id);
            var viewModel = AutoMapper.Mapper.Map<ProductsDetailsViewModel>(serviceModel);
            var ingNames = this.productsService.GetAllIngredientsName(serviceModel);

            this.ViewData[ServicesGlobalConstants.IngredientsNameViewData] = ingNames;

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Search(string searchString)
        {
            GetAllProductTypes();

            var searchedProducts = this.productsService.Search(searchString)
                .Select(product => product.To<ProductViewModel>())
                .ToList();

            return this.View(ServicesGlobalConstants.ViewProductsAll, searchedProducts);
        }

        [Authorize]
        public IActionResult Order(ProductOrderInputModel inputModel, string id)
        {
            OrderServiceModel orderServiceModel = inputModel.To<OrderServiceModel>();

            orderServiceModel.IssuerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            orderServiceModel.ProductId = id;
            this.ordersService.Create(orderServiceModel);

            return this.RedirectToAction("Cart", "Orders");
        }

        private void GetAllProductTypes()
        {
            var allProductTypes = this.productsService.GetAllTypes();

            this.ViewData[ServicesGlobalConstants.ProductTypesMenuViewData] = allProductTypes
                .Select(productType => new ProductTypeViewModel
            {
                Id = productType.Id,
                Name = productType.Name
            });
        }
    }
}