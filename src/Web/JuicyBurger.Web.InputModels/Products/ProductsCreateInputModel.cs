using JuicyBurger.Services.GlobalConstants;
using AutoMapper;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JuicyBurger.Web.InputModels.Products
{
    public class ProductsCreateInputModel : IMapTo<ProductsCreateInputServiceModel>, IMapFrom<ProductsCreateInputServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = ServicesGlobalConstants.ModelNameErrorMessage)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), ServicesGlobalConstants.DecimalMinValue, ServicesGlobalConstants.DecimalMaxValue, ErrorMessage = ServicesGlobalConstants.ProductsCreateInputModelPriceErrorMessage)]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public int ProductTypeId { get; set; }

        [Required(ErrorMessage =ServicesGlobalConstants.ProductsCreateInputModelProductTypeErrorMessage)]
        public string ProductType { get; set; }

        [Required]
        public List<string> Ingredients { get; set; }

        //public ICollection<Review> Reviews { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
             .CreateMap<ProductsCreateInputModel, ProductsCreateInputServiceModel>()
                         .ForMember(destination => destination.ProductType,
                         opts => opts.Ignore());
        }
    }
}
