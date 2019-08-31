using JuicyBurger.Service.Products;
using JuicyBurger.Services.Cloud;
using JuicyBurger.Services.GlobalConstants;
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
            SetProductTypeViewData();

            SetIngredientsViewData();

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(ProductsCreateInputModel serviceModel)
        {
            if (!ModelState.IsValid)
            {
                SetProductTypeViewData();

                SetIngredientsViewData();

                return this.View(serviceModel);
            }

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

            this.productsServices.Create(product);

            return this.Redirect(ServicesGlobalConstants.HomeIndex); //redirect ot products all
        }

        [HttpGet(ServicesGlobalConstants.TypeCreateRoute)]
        public IActionResult CreateType()
        {
            this.productsServices.GetAllTypes();

            return this.View(ServicesGlobalConstants.ReturnTypeCreateView);
        }

        [HttpPost(ServicesGlobalConstants.TypeCreateRoute)]
        public IActionResult CreateType(ProductTypesCreateInputModel serviceModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(serviceModel);
            }

            ProductTypeServiceModel product = new ProductTypeServiceModel
            {
                Name = serviceModel.Name,
            };

            this.productsServices.CreateType(product);

            return this.Redirect(ServicesGlobalConstants.RedirectProductCreate);
        }

        private void SetIngredientsViewData()
        {
            var allIngredients = this.ingredientsServices.GetAll();
            this.ViewData[ServicesGlobalConstants.IngredientsViewData] = allIngredients.Select(ingredient => new IngredientsAllCreateViewModel
            {
                Name = ingredient.Name
            });
        }

        private void SetProductTypeViewData()
        {
            var allProductTypes = this.productsServices.GetAllTypes();
            this.ViewData[ServicesGlobalConstants.ProductTypeViewData] = allProductTypes.Select(productType => new ProductTypeViewModel
            {
                Name = productType.Name
            });
        }
    }
}
