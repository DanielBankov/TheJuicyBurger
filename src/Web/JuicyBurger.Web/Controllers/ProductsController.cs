using JuicyBurger.Service.Products;
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

        [Authorize]
        [HttpGet("/Products/All/{id}")]
        public IActionResult All(int id)
        {
            GetAllProductTypes();

            var products = this.productsService.All(id)
                .Select(prod => prod.To<ProductViewModel>())
                .ToList();

            return this.View(products);
        }

        [Authorize]
        [Route("/Products/Details/{id}")]
        public IActionResult Details(string id)
        {
            var serviceModel = this.productsService.Details(id);

            var viewModel = AutoMapper.Mapper.Map<ProductsDetailsViewModel>(serviceModel);

            var ingNames = this.productsService.GetAllIngredientsName(serviceModel);

            this.ViewData["ingredientsName"] = ingNames;

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Search(string searchString)
        {
            GetAllProductTypes();

            var searchedProducts = this.productsService.Search(searchString)
                .Select(product => product.To<ProductViewModel>())
                .ToList();

            return this.View("All", searchedProducts);
        }

        [HttpPost(Name = "Order")]
        public IActionResult Order(ProductOrderInputModel inputModel, string id)
        {
            OrderServiceModel orderServiceModel = inputModel.To<OrderServiceModel>();

            orderServiceModel.IssuerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            orderServiceModel.ProductId = id;
            this.ordersService.Create(orderServiceModel);

            //retur to cart
            return this.Redirect("/");
        }

        private void GetAllProductTypes()
        {
            var allProductTypes = this.productsService.GetAllTypes();

            this.ViewData["productTypesMenu"] = allProductTypes.Select(productType => new ProductTypeViewModel
            {
                Id = productType.Id,
                Name = productType.Name
            });

            //validate if productTypes are null
        }
    }
}