using AutoMapper;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;
using JuicyBurger.Web.ViewModels.Products;
using Microsoft.AspNetCore.Http;

namespace JuicyBurger.Web.InputModels.Products
{
    public class ProductsCreateInputModel : IMapTo<ProductServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public int ProductTypeId { get; set; }

        public string ProductType { get; set; }

        //public List<ProductIngredientModel> ProductIngredients { get; set; }

        // public ICollection<Review> Reviews { get; set; }
        //
        // public ICollection<OrderProduct> OrderProducts { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<ProductsCreateInputModel, ProductServiceModel>()
                .ForMember(destination => destination.ProductType,
                            opts => opts.MapFrom(origin => new ProductTypeServiceModel { Name = origin.ProductType }));
        }
    }
}
