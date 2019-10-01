using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Service.Products;
using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Ingredients;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JuicyBurger.Service
{
    public class ProductsService : IProductsService
    {
        private readonly int num = ServicesGlobalConstants.ComparisonNumberForResultFromDbSaveChanges;

        private readonly JuicyBurgerDbContext context;
        private readonly IIngredientsService ingredientsService;

        public ProductsService(JuicyBurgerDbContext context, IIngredientsService ingredientsService)
        {
            this.context = context;
            this.ingredientsService = ingredientsService;
        }

        public IQueryable<ProductServiceModel> AllByProductTypeId(int? id)
        {
            return id == null ? 
                this.context.Products.Where(product => product.ProductTypeId == 1).To<ProductServiceModel>() : this.context.Products.Where(product => product.ProductTypeId == id).To<ProductServiceModel>();
        }

        public bool Create(ProductServiceModel inputModel)
        {
            ProductType productTypeDb = this.context.ProductTypes
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

            this.context.ProductTypes.Add(productType);
            int result = this.context.SaveChanges();

            return result > num;
        }

        public bool Delete(string id)
        {
            var productDb = this.context.Products.SingleOrDefault(product => product.Id == id);
            productDb.IsDeleted = true;

            context.Products.Update(productDb);
            var result = context.SaveChanges();

            return result > num;
        }

        public ProductsDetailsServiceModel Details(string id)
        {
            Product dbProduct = this.context.Products.Where(product => product.Id == id).FirstOrDefault();

            var serviceModel = AutoMapper.Mapper.Map<ProductsDetailsServiceModel>(dbProduct);

            return serviceModel;
        }

        public IQueryable<ProductsAllServiceModel> GetAll()
        {
            return this.context.Products.Where(product => product.IsDeleted == false).To<ProductsAllServiceModel>();
        }

        public string GetAllIngredientsName(ProductsDetailsServiceModel serviceModel)
        {
            Product productWithIngredients = this.context
                .Products
                .Include(p => p.ProductIngredients)
                .SingleOrDefault(p => p.Id == serviceModel.Id);

            var ingredientsIds = this.ingredientsService.GetAllIds(productWithIngredients);
            var ingredientsName = this.ingredientsService.IngredientsStringNames(ingredientsIds);

            return ingredientsName;
        }

        public IQueryable<ProductTypeServiceModel> GetAllTypes()
        {
            var result = this.context.ProductTypes
                .Select(productType => new ProductTypeServiceModel
                {
                    Id = productType.Id,
                    Name = productType.Name
                });

            return result;
        }

        public IQueryable<ProductServiceModel> Search(string searchString)
        {
            return this.context.Products
                .Where(product => product.Name.Contains(searchString))
                .To<ProductServiceModel>();
        }
    }
}

