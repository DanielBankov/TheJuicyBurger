using JuicyBurger.Service.Products;
using JuicyBurger.Services.Cloud;
using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Ingredients;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Ingredients;
using JuicyBurger.Services.Models.Products;
using JuicyBurger.Web.InputModels.Products;
using JuicyBurger.Web.ViewModels.Ingredients;
using JuicyBurger.Web.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet("/Administration/Products/All")]
        public async Task<IActionResult> All()
        {
            var products = await this.productsServices.GetAll()
                .To<ProductsAllViewModel>()
                .ToListAsync(); // where is active

            return this.View(products);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.productsServices.Delete(id);

            return this.Redirect(ServicesGlobalConstants.HomeIndex);
        }

        public async Task<IActionResult> Create()
        {
            await SetProductTypeViewData();

            await SetIngredientsViewData();

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductsCreateInputModel serviceModel)
        {
            if (!ModelState.IsValid)
            {
                await SetProductTypeViewData();

                await SetIngredientsViewData();

                return this.View(serviceModel);
            }

            string imageUrl = this.cloudinaryServices.UploadeImage(serviceModel.Image, serviceModel.Name);

            var productType = await this.productsServices.GetAllTypes().FirstOrDefaultAsync(pt => pt.Name == serviceModel.ProductType);

            var productServiceModel = AutoMapper.Mapper.Map<ProductsCreateInputServiceModel>(serviceModel);
            productServiceModel.ProductType = productType;

            List<IngredientServiceModel> ingredientServiceModels =  await this.ingredientsServices.MapIngNamesToIngredientServiceModel(productServiceModel);

            ProductServiceModel product = new ProductServiceModel
            {
                Name = serviceModel.Name,
                Ingredients = ingredientServiceModels,
                Description = serviceModel.Description,
                Price = serviceModel.Price,
                ProductType = productType,
                Image = imageUrl
            };

            await this.productsServices.Create(product);

            return this.Redirect(ServicesGlobalConstants.HomeIndex); 
        }

        [HttpGet(ServicesGlobalConstants.TypeCreateRoute)]
        public async Task<IActionResult> CreateType()
        {
            this.productsServices.GetAllTypes();

            return this.View(ServicesGlobalConstants.ReturnTypeCreateView);
        }

        [HttpPost(ServicesGlobalConstants.TypeCreateRoute)]
        public async Task<IActionResult> CreateType(ProductTypesCreateInputModel serviceModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(serviceModel);
            }

            ProductTypeServiceModel product = new ProductTypeServiceModel
            {
                Name = serviceModel.Name,
            };

            await this.productsServices.CreateType(product);

            return this.Redirect(ServicesGlobalConstants.RedirectProductCreate);
        }

        private Task SetIngredientsViewData()
        {
            var allIngredients = this.ingredientsServices.GetAll();
            this.ViewData[ServicesGlobalConstants.IngredientsViewData] = allIngredients.Select(ingredient => new IngredientsAllCreateViewModel
            {
                Name = ingredient.Name
            });

            return Task.CompletedTask;
        }

        private Task SetProductTypeViewData()
        {
            var allProductTypes = this.productsServices.GetAllTypes();
            this.ViewData[ServicesGlobalConstants.ProductTypeViewData] = allProductTypes.Select(productType => new ProductTypeViewModel
            {
                Name = productType.Name
            });

            return Task.CompletedTask;
        }
    }
}
