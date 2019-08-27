using JuicyBurger.Service.Products;
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
        private readonly IOrdersService ordersService;

        public ProductsController(IProductsService productsService, IOrdersService OrdersService)
        {
            this.productsService = productsService;
            this.ordersService = OrdersService;
        }

        [HttpGet("/Products/All/{id}")]
        public IActionResult All(int id)
        {
            GetAllProductTypes();

            var products = productsService.All(id)
                .Select(product => new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ProductTypeId = product.ProductTypeId,
                    Price = product.Price,
                    Image = product.Image
                })
                .ToList();

            return View(products);
        }

        public IActionResult Details(string id)
        {
            var serviceModel = productsService.Details(id);

            var viewModel = new ProductsDetailsViewModel
            {
                Id = serviceModel.Id,
                Name = serviceModel.Name,
                Carbohydrates = serviceModel.Carbohydrates,
                Description = serviceModel.Description,
                Price = serviceModel.Price,
                Image = serviceModel.Image,
                Fat = serviceModel.Fat,
                Proteins = serviceModel.Proteins,
                TotalCalories = serviceModel.TotalCalories
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Search(string searchString)
        {
            GetAllProductTypes();

            var searchedProducts = productsService.Search(searchString)
                .Select(product => new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ProductTypeId = product.ProductTypeId,
                    Price = product.Price,
                    Image = product.Image
                })
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