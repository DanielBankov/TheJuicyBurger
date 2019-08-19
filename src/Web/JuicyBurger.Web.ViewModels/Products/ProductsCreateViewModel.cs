namespace JuicyBurger.Web.ViewModels.Products
{
    public class ProductsCreateViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }


        public double Weight { get; set; }

        public decimal Price { get; set; }

        //public double Carbohydrates { get; set; }

        //public double Fat { get; set; }

        //public double Proteins { get; set; }

        //public double TotalCalories { get; set; }

        public string Image { get; set; }

        public int ProductTypeId { get; set; }

        public ProductTypeViewModel ProductType { get; set; }

       //public ICollection<ProductIngredient> ProductIngredients { get; set; }
       //
       //public ICollection<Review> Reviews { get; set; }
       //
       //public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
