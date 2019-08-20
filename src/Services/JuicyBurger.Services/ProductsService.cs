using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.Models;
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

        public IQueryable All()
        {
            return this.context.Products.Select(x => x); // make service entity return type
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
    }
}

