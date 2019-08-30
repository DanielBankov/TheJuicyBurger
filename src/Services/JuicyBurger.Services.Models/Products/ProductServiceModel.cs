using AutoMapper;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Ingredients;
using System.Collections.Generic;
using System.Linq;

namespace JuicyBurger.Services.Models.Products
{
    public class ProductServiceModel : IMapFrom<Product>, IMapTo<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int ProductTypeId { get; set; }

        public ProductTypeServiceModel ProductType { get; set; }

        public List<IngredientServiceModel> Ingredients { get; set; }
    }
}
