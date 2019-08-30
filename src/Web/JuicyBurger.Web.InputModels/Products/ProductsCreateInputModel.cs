using AutoMapper;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Ingredients;
using JuicyBurger.Services.Models.Products;
using JuicyBurger.Web.InputModels.Ingredients;
using JuicyBurger.Web.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace JuicyBurger.Web.InputModels.Products
{
    public class ProductsCreateInputModel : IMapTo<ProductServiceModel>, IMapFrom<ProductServiceModel>, IHaveCustomMappings
    {
        //public ProductsCreateInputModel()
        //{
        //    this.Ingredients = new List<IngredientsCreateInputModel>();
        //}

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public int ProductTypeId { get; set; }

        public string ProductType { get; set; }

        public List<string> Ingredients { get; set; }

        //public ICollection<Review> Reviews { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<ProductsCreateInputModel, ProductServiceModel>()
                .ForMember(destination => destination.ProductType,
                            opts => opts.MapFrom(origin => new ProductTypeServiceModel { Name = origin.ProductType }));
                 //.ForMember(destination => destination.Ingredients,
                 //           opts => opts.MapFrom(origin => new List<IngredientServiceModel> { new IngredientServiceModel { Name = origin.Ingredients } }));
        }
    }
}
