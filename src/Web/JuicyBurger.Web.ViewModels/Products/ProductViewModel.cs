using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Products;
using JuicyBurger.Web.ViewModels.Ingredients;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuicyBurger.Web.ViewModels.Products
{
    public class ProductViewModel : IMapTo<ProductServiceModel>, IMapFrom<ProductServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int ProductTypeId { get; set; }

        public ProductTypeViewModel ProductType { get; set; }

        public List<IngredientsProductViewModel> Ingredients { get; set; }
    }
}
