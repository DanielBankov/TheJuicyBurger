using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Service.Products;
using JuicyBurger.Services.Models;
using JuicyBurger.Services.Models.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuicyBurger.Service
{
    public class ProductsService : IProductsService
    {
        private readonly JuicyBurgerDbContext context;

        public ProductsService(JuicyBurgerDbContext context)
        {
            this.context = context;
        }

        public IQueryable<ProductAllServiceModel> All(int id)
        {
            return this.context.Products
                .Where(type => type.ProductType.Id == id)
                .Select(product => new ProductAllServiceModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ProductType = product.ProductType.Name,
                    ProductTypeId = product.ProductTypeId,
                    Image = product.Image,
                    Quantity = product.Quantity,
                    Weight = product.Weight
                });
        }

        public bool Create(ProductsCreateInputServiceModel inputModel)
        {
            ProductType productTypeDb = context.ProductTypes.FirstOrDefault(type => type.Name == inputModel.ProductType.Name); //

            Product product = new Product
            {
                Name = inputModel.Name,
                Price = inputModel.Price,
                Weight = inputModel.Weight,
                Image = inputModel.Image,
                Quantity = inputModel.Quantity,
                Description = inputModel.Description,
                ProductType = productTypeDb
            };

            context.Products.Add(product);
            int result = context.SaveChanges();

            return result > 0;
        }

        public bool CreateType(ProductTypeServiceModel inputModel)
        {
            ProductType productType = new ProductType
            {
                Name = inputModel.Name
            };

            context.ProductTypes.Add(productType);
            int result = context.SaveChanges();

            return result > 0;
        }

        public ProductsDetailsServiceModel Details(string id)
        {
            var dbProduct = context.Products.Where(product => product.Id == id).FirstOrDefault();

            var serviceModel = new ProductsDetailsServiceModel
            {
                Id = dbProduct.Id,
                Name = dbProduct.Name,
                Carbohydrates = dbProduct.Carbohydrates,
                Description = dbProduct.Description,
                Price = dbProduct.Price,
                Image = dbProduct.Image,
                Fat = dbProduct.Fat,
                Proteins = dbProduct.Proteins,
                TotalCalories = dbProduct.TotalCalories,
                Quantity = dbProduct.Quantity,
                Weight = dbProduct.Weight
            };

            return serviceModel;
        }

        public IQueryable<ProductTypeServiceModel> GetAllTypes()
        {
            var result = context.ProductTypes
                .Select(productType => new ProductTypeServiceModel
                {
                    Id = productType.Id,
                    Name = productType.Name
                });

            return result;
        }

        public IQueryable<ProductAllServiceModel> Search(string searchString)
        {
            return context.Products
                .Where(product => product.Name.Contains(searchString))
                .Select(product => new ProductAllServiceModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ProductType = product.ProductType.Name,
                    ProductTypeId = product.ProductTypeId,
                    Image = product.Image,
                    Quantity = product.Quantity,
                    Weight = product.Weight
                });
        }
    }
}

