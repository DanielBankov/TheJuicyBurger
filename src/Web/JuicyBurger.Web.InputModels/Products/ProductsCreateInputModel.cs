using JuicyBurger.Web.ViewModels.Products;
using Microsoft.AspNetCore.Http;

namespace JuicyBurger.Web.InputModels.Products
{
    public class ProductsCreateInputModel
    {
        public string Id { get; set; }

        public string Name { get; set; }


        public double Weight { get; set; }

        public decimal Price { get; set; }

        public IFormFile Image { get; set; }

        public int ProductTypeId { get; set; }

        public string ProductType { get; set; }

        //public List<ProductIngredientModel> ProductIngredients { get; set; }

       // public ICollection<Review> Reviews { get; set; }
       //
       // public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
