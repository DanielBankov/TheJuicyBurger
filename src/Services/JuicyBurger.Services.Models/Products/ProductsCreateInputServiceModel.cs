using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;

namespace JuicyBurger.Services.Models.Products
{
    public class ProductsCreateInputServiceModel : IMapFrom<Product>, IMapTo<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Weight { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public int ProductTypeId { get; set; }

        public ProductTypeServiceModel ProductType { get; set; }

        //public List<ProductIngredientModel> ProductIngredients { get; set; }

        // public ICollection<Review> Reviews { get; set; }
        //
        // public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
