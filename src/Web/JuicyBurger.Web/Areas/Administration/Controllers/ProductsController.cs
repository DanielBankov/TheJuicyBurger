using JuicyBurger.Service.Products;
using JuicyBurger.Services.Cloud;
using JuicyBurger.Services.Ingredients;
using JuicyBurger.Services.Models.Ingredients;
using JuicyBurger.Services.Models.Products;
using JuicyBurger.Web.InputModels.Products;
using JuicyBurger.Web.ViewModels.Ingredients;
using JuicyBurger.Web.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace JuicyBurger.Web.Areas.Administration.Controllers
{
    public class ProductsController : AdminController
    {
        private readonly IProductsService productsServices;
        private readonly IIngredientsService ingredientsServices;
        private readonly ICloudinaryService cloudinaryServices;

        public ProductsController(IProductsService productsServices, IIngredientsService ingredientsServices, ICloudinaryService cloudinaryServices)
        {
            this.productsServices = productsServices;
            this.ingredientsServices = ingredientsServices;
            this.cloudinaryServices = cloudinaryServices;
        }

        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            var allProductTypes = this.productsServices.GetAllTypes();
            this.ViewData["productTypes"] = allProductTypes.Select(productType => new ProductTypeViewModel
            {
                Name = productType.Name
            });

            var allIngredients = this.ingredientsServices.GetAll();
            this.ViewData["ingredients"] = allIngredients.Select(ingredient => new IngredientsAllCreateViewModel
            {
                Name = ingredient.Name
            });

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(ProductsCreateInputModel serviceModel)
        {
            string imageUrl = this.cloudinaryServices.UploadeImage(serviceModel.Image, serviceModel.Name);

            var productType = this.productsServices.GetAllTypes().FirstOrDefault(pt => pt.Name == serviceModel.ProductType);

            var productServiceModel = AutoMapper.Mapper.Map<ProductsCreateInputServiceModel>(serviceModel);
            productServiceModel.ProductType = productType;

            List<IngredientServiceModel> ingredientServiceModels = this.ingredientsServices.MapIngNamesToIngredientServiceModel(productServiceModel);

            ProductServiceModel product = new ProductServiceModel
            {
                Name = serviceModel.Name,
                Ingredients = ingredientServiceModels,
                Description = serviceModel.Description,
                Price = serviceModel.Price,
                ProductType = productType,
                Image = imageUrl
            };

            //try catch

            string productId = this.productsServices.Create(product);

            return this.Redirect("/"); //redirect ot products all
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

            return this.View("Product/Create");
        }

    }
}
