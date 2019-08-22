using JuicyBurger.Service;
using JuicyBurger.Web.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JuicyBurger.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet("/Products/All/{id}")]
        public IActionResult All(int id)
        {
            GetAllProductTypes();

            var products = productsService.All(id)
                .Select(product => new ProductAllViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    ProductType = product.ProductType,
                    ProductTypeId = product.ProductTypeId,
                    Price = product.Price,
                    Image = product.Image,
                    Quantity = product.Quantity,
                    Weight = product.Weight
                })
                .ToList();

            return View(products);
        }

        [HttpPost]
        public IActionResult Search(string searchString)
        {
            GetAllProductTypes();



            var searchedProducts = productsService.Search(searchString)
                .Select(product => new ProductAllViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    ProductType = product.ProductType,
                    ProductTypeId = product.ProductTypeId,
                    Price = product.Price,
                    Image = product.Image,
                    Quantity = product.Quantity,
                    Weight = product.Weight
                })
                .ToList();

            return View("All", searchedProducts);
        }

        public IActionResult BurgersMenu()
        {
            return View();
        }

        public IActionResult FriesMenu()
        {
            return View();
        }

        public IActionResult DrinksMenu()
        {
            return View();
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
    }
}