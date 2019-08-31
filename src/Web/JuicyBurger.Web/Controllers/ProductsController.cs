using JuicyBurger.Service.Products;
using JuicyBurger.Services.Ingredients;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Orders;
using JuicyBurger.Web.ViewModels.Products;
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

        [HttpGet("/Products/All/{id}")]
        public IActionResult All(int id)
        {
            GetAllProductTypes();

            var products = productsService.All(id)
                .Select(prod => prod.To<ProductViewModel>())
                .ToList();

            return View(products);
        }

        [Route("/Products/Details/{id}")]
        public IActionResult Details(string id)
        {
            var serviceModel = productsService.Details(id);

            var viewModel = AutoMapper.Mapper.Map<ProductsDetailsViewModel>(serviceModel);

            var ingNames = this.productsService.GetAllIngredientsName(serviceModel);

            this.ViewData["ingredientsName"] = ingNames;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Search(string searchString)
        {
            GetAllProductTypes();

            var searchedProducts = productsService.Search(searchString)
                .Select(product => product.To<ProductViewModel>())
                .ToList();

            return View("All", searchedProducts);
        }

        private void GetAllProductTypes()
            {
            var allProductTypes = this.productsService.GetAllTypes();

            this.ViewData["productTypesMenu"] = allProductTypes.Select(productType => new ProductTypeViewModel
            {
                Id = productType.Id,
                Name = productType.Name
            });

            this.ViewData["productTypeAllProductsIndex"] = allProductTypes.SingleOrDefault().Id;

            //validate if productTypes are null
        }

        [HttpPost(Name = "Order")]
        public IActionResult Order(string id, string comment)
        {
            var issuer = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var order = ordersService.Create(id, comment, issuer);


            //retur to cart
            return this.Redirect("/");
        }
    }
}