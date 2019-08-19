namespace JuicyBurger.Services.Models
{
    public class ProductsCreateInputServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public int ProductTypeId { get; set; }

        public ProductTypeServiceModel ProductType { get; set; }

        //public List<ProductIngredientModel> ProductIngredients { get; set; }

        // public ICollection<Review> Reviews { get; set; }
        //
        // public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
