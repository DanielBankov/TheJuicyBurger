using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Ingredients;
using System.Collections.Generic;

namespace JuicyBurger.Services.Models.Products
{
    public class ProductsDetailsServiceModel : IMapFrom<Product>, IMapTo<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Carbohydrates { get; set; }

        public double Fat { get; set; }

        public double Proteins { get; set; }

        public bool IsDeleted { get; set; }

        public double TotalCalories { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int ProductTypeId { get; set; }

        public string ProductType { get; set; }

        public List<IngredientServiceModel> Ingredients { get; set; }
    }
}
