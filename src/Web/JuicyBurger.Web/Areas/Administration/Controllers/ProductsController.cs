﻿using JuicyBurger.Services;
using JuicyBurger.Services.Models;
using JuicyBurger.Web.InputModels.Products;
using JuicyBurger.Web.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JuicyBurger.Web.Areas.Administration.Controllers
{
    public class ProductsController : AdminController
    {
        private readonly IProductsServices productsServices;

        public ProductsController(IProductsServices productsServices)
        {
            this.productsServices = productsServices;
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
                Name = productType.Name,
            });

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(ProductsCreateInputModel serviceModel)
        {
            ProductsCreateInputServiceModel product = new ProductsCreateInputServiceModel
            {
                Name = serviceModel.Name,
                Price = serviceModel.Price,
                Weight = serviceModel.Weight,
                ProductType = new ProductTypeServiceModel
                {
                    Name = serviceModel.ProductType,
                    Id = serviceModel.ProductTypeId
                }
            };

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
