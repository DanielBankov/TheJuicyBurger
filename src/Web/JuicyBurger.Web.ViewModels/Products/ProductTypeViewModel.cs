using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;

namespace JuicyBurger.Web.ViewModels.Products
{
    public class ProductTypeViewModel : IMapFrom<ProductTypeServiceModel>, IMapTo<ProductTypeServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
