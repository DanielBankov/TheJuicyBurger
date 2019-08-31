using AutoMapper;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;
using JuicyBurger.Services.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JuicyBurger.Web.InputModels.Products
{
    public class ProductsCreateInputModel : IMapTo<ProductsCreateInputServiceModel>, IMapFrom<ProductsCreateInputServiceModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Name must contains at least 3 symbols and max 150!")]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "Price must be at least 0.01!")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public int ProductTypeId { get; set; }

        [Required(ErrorMessage = "Product Type must be chosen!")]
        public string ProductType { get; set; }

        [Required]
        [AtLeastOneIngredient]
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
