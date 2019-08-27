using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;

namespace JuicyBurger.Services.Models.Products
{
    public class ProductTypeServiceModel : IMapFrom<ProductType>, IMapTo<ProductType>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
