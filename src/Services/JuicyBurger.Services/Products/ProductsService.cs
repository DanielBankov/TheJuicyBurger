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

            var IsProductExists = this.context.Products.Any(prod => prod.Name == inputModel.Name);

            if (IsProductExists)
            {
                throw new InvalidOperationException("Product with this name already exists!");
            }

            Product product = new Product
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                Price = inputModel.Price,
                ProductType = productTypeDb,
                Image = inputModel.Image
            };

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

            //TODO: Map with AutoMapper
            ProductsDetailsServiceModel serviceModel = new ProductsDetailsServiceModel
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
            };

            return serviceModel;
        }

        public string GetAllIngredientsName(ProductsDetailsServiceModel serviceModel)
        {
            Product productWithIngredients = this.context.Products
                .Include(p => p.ProductIngredients)
                .SingleOrDefault(p => p.Id == serviceModel.Id);

            var ingredientsIds = this.ingredientsService.GetAllIds(productWithIngredients);

            StringBuilder sb = new StringBuilder();

            var ingredients = this.ingredientsService.GetAll().ToList();

            for (int i = 0; i < ingredients.Count; i++)
            {
                if (ingredientsIds.Contains(ingredients[i].Id))
                {
                    sb.Append($"{ingredients[i].Name}, ");
                }
            }

            string ingredientsName = sb.ToString().TrimEnd();
            string removeLastComma = ingredientsName.Remove(ingredientsName.Length - 1, 1);

            return removeLastComma;
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
            //TODO: Map with AutoMapper
            return context.Products
                .Where(product => product.Name.Contains(searchString))
                .Select(product => new ProductServiceModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ProductTypeId = product.ProductTypeId,
                    Image = product.Image,
                });
        }
    }
}

