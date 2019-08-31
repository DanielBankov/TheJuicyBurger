using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;

namespace JuicyBurger.Web.ViewModels.Products
{
    public class ProductsAllViewModel : IMapTo<ProductsAllServiceModel>, IMapFrom<ProductsAllServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool IsDeleted { get; set; }

        public int ProductTypeId { get; set; }

        public ProductTypeViewModel ProductType { get; set; }
    }
}
