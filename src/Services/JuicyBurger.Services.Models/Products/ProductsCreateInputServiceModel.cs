using AutoMapper;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Ingredients;
using System.Collections.Generic;

namespace JuicyBurger.Services.Models.Products
{
    public class ProductsCreateInputServiceModel : IMapFrom<Product>, IMapTo<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public int ProductTypeId { get; set; }

        public ProductTypeServiceModel ProductType { get; set; }

        public List<string> Ingredients { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration
        //     .CreateMap<ProductsCreateInputServiceModel, Product>()
        //     .ForMember(destination => destination.ProductIngredients,
        //                 opts => opts.Ignore())
        //                 .ForMember(destination => destination.ProductType,
        //                 opts => opts.Ignore());
        //}
    }
}
