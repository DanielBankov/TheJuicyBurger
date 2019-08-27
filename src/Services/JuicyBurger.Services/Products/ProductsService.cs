using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Service.Products;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;
using System.Linq;

namespace JuicyBurger.Service
{
    public class ProductsService : IProductsService
    {
        private readonly JuicyBurgerDbContext context;

        public ProductsService(JuicyBurgerDbContext context)
        {
            this.context = context;
        }

        public IQueryable<ProductServiceModel> All(int id)
        {
            return this.context.Products.Where(product => product.ProductTypeId == id).To<ProductServiceModel>();
        }

        public bool Create(ProductServiceModel inputModel)
        {
            ProductType productTypeDb = context.ProductTypes
                .SingleOrDefault(type => type.Name == inputModel.ProductType.Name);

            Product product = AutoMapper.Mapper.Map<Product>(inputModel);

            product.ProductType = productTypeDb;

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
                TotalCalories = dbProduct.TotalCalories
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

        public IQueryable<ProductServiceModel> Search(string searchString)
        {
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

