using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Service.Products;
using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Ingredients;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<bool> Create(ProductServiceModel inputModel)
        {
            ProductType productTypeDb = await this.context.ProductTypes
                    .SingleOrDefaultAsync(type => type.Name == inputModel.ProductType.Name);

            var product = AutoMapper.Mapper.Map<Product>(inputModel);

            var result = await ingredientsService.SetIngredientsToProduct(product, inputModel.Ingredients);

            return result;
        }

        public async Task<bool> CreateType(ProductTypeServiceModel inputModel)
        {
            ProductType productType = new ProductType
            {
                Name = inputModel.Name
            };

            await this.context.ProductTypes.AddAsync(productType);
            int result = await this.context.SaveChangesAsync();

            return result > num;
        }

        public async Task<bool> Delete(string id)
        {
            var productDb = await this.context.Products.SingleOrDefaultAsync(product => product.Id == id);
            productDb.IsDeleted = true;

            await Task.Run(() => context.Products.Update(productDb));
            var result = await context.SaveChangesAsync();

            return result > num;
        }

        public async Task<ProductsDetailsServiceModel> Details(string id)
        {
            Product dbProduct = await this.context.Products.Where(product => product.Id == id).FirstOrDefaultAsync();

            var serviceModel = AutoMapper.Mapper.Map<ProductsDetailsServiceModel>(dbProduct);

            return serviceModel;
        }

        public IQueryable<ProductsAllServiceModel> GetAll()
        {
            return this.context.Products.Where(product => product.IsDeleted == false).To<ProductsAllServiceModel>();
        }

        public async Task<string> GetAllIngredientsName(ProductsDetailsServiceModel serviceModel)
        {
            Product productWithIngredients = await this.context
                .Products
                .Include(p => p.ProductIngredients)
                .SingleOrDefaultAsync(p => p.Id == serviceModel.Id);

            var ingredientsIds = await this.ingredientsService.GetAllIds(productWithIngredients);
            var ingredientsName = await this.ingredientsService.IngredientsStringNames(ingredientsIds);

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

