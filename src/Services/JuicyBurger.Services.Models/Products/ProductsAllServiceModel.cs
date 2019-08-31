using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;

namespace JuicyBurger.Services.Models.Products
{
    public class ProductsAllServiceModel : IMapTo<Product>, IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int ProductTypeId { get; set; }

        public bool IsDeleted { get; set; }

        public ProductTypeServiceModel ProductType { get; set; }
    }
}
