using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;
using JuicyBurger.Web.ViewModels.Ingredients;
using System.Collections.Generic;

namespace JuicyBurger.Web.ViewModels.Products
{
    public class ProductsDetailsViewModel : IMapFrom<ProductServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Carbohydrates { get; set; }

        public double Fat { get; set; }

        public double Proteins { get; set; }

        public double TotalCalories { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int ProductTypeId { get; set; }

        public string ProductType { get; set; }

        public List<IngredientsProductViewModel> Ingredients { get; set; }

    }
}
