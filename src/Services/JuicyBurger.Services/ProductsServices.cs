using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuicyBurger.Services
{
    public class ProductsServices : IProductsServices
    {
        private readonly JuicyBurgerDbContext context;

        public ProductsServices(JuicyBurgerDbContext context)
        {
            this.context = context;
        }

        public IQueryable All()
        {
            return this.context.Products.Select(x => x); // make service entity return type
        }

        public bool Create(ProductsCreateInputServiceModel inputModel)
        {
            Product product = new Product
            {
                Name = inputModel.Name,
                Price = inputModel.Price,
                Weight = inputModel.Weight,

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

