using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Service.Products;
using JuicyBurger.Services.Ingredients;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System;
using Services.Exceptions;
using Services.Exceptions.Messages;

namespace JuicyBurger.Service
{
    public class ProductsService : IProductsService
    {
        private readonly JuicyBurgerDbContext context;
        private readonly IIngredientsService ingredientsService;

        public ProductsService(JuicyBurgerDbContext context, IIngredientsService ingredientsService)
        {
            this.context = context;
            this.ingredientsService = ingredientsService;
        }

        public IQueryable<ProductServiceModel> All(int id)
        {
            return this.context.Products.Where(product => product.ProductTypeId == id).To<ProductServiceModel>();
        }

        public bool Create(ProductServiceModel inputModel)
        {
            ProductType productTypeDb = context.ProductTypes
                    .SingleOrDefault(type => type.Name == inputModel.ProductType.Name);

            var product = AutoMapper.Mapper.Map<Product>(inputModel);

            var result = ingredientsService.SetIngredientsToProduct(product, inputModel.Ingredients);
            return result;
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
            Product dbProduct = context.Products.Where(product => product.Id == id).FirstOrDefault();

            var serviceModel = AutoMapper.Mapper.Map<ProductsDetailsServiceModel>(dbProduct);

            return serviceModel;
        }

        public string GetAllIngredientsName(ProductsDetailsServiceModel serviceModel)
        {
            Product productWithIngredients = this.context.Products
                .Include(p => p.ProductIngredients)
                .SingleOrDefault(p => p.Id == serviceModel.Id);

            var ingredientsIds = this.ingredientsService.GetAllIds(productWithIngredients);
            var ingredientsName = this.ingredientsService.IngredientsStringNames(ingredientsIds);

            return ingredientsName;
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

        public IQueryable<ProductServiceModel> Search(string searchString)
        {
            return context.Products
                .Where(product => product.Name.Contains(searchString))
                .To<ProductServiceModel>();
        }
    }
}

