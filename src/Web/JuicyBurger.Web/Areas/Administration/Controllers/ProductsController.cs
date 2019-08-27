using JuicyBurger.Service.Products;
using JuicyBurger.Services.Cloud;
using JuicyBurger.Services.Models.Products;
using JuicyBurger.Web.InputModels.Products;
using JuicyBurger.Web.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JuicyBurger.Web.Areas.Administration.Controllers
{
    public class ProductsController : AdminController
    {
        private readonly IProductsService productsServices;

        private readonly ICloudinaryService cloudinaryServices;

        public ProductsController(IProductsService productsServices, ICloudinaryService cloudinaryServices)
        {
            this.productsServices = productsServices;
            this.cloudinaryServices = cloudinaryServices;
        }

        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            var allProductTypes =  this.productsServices.GetAllTypes();

            this.ViewData["productTypes"] = allProductTypes.Select(productType => new ProductTypeViewModel
            {
                Name = productType.Name
            });

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(ProductsCreateInputModel serviceModel)
        {
            string imageUrl = this.cloudinaryServices.UploadeImage(serviceModel.Image, serviceModel.Name);

            ProductServiceModel product = AutoMapper.Mapper.Map<ProductServiceModel>(serviceModel);
            product.Image = imageUrl;

            this.productsServices.Create(product);

            return this.Redirect("/");
        }

        [HttpGet("/Administration/Products/Type/Create")]
        public IActionResult CreateType()
        {
            this.productsServices.GetAllTypes();

            return this.View("Type/Create");
        }

        [HttpPost("/Administration/Products/Type/Create")]
        public IActionResult CreateType(ProductTypesCreateInputModel serviceModel)
        {
            ProductTypeServiceModel product = new ProductTypeServiceModel
            {
                Name = serviceModel.Name,
            };

            this.productsServices.CreateType(product);

            return this.Redirect("Create");
        }

    }
}
