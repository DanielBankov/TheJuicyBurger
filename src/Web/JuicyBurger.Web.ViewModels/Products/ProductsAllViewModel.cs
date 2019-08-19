namespace JuicyBurger.Web.ViewModels.Products
{
    public class ProducstAllViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool InStock { get; set; }

        public double Weight { get; set; }

        public decimal Price { get; set; }

        public double Carbohydrates { get; set; }

        public double Fat { get; set; }

        public double Proteins { get; set; }

        public double TotalCalories { get; set; }

        public string Image { get; set; }

        public int ProductTypeId { get; set; }

        public string ProductType { get; set; }//

        public string ProductIngredients { get; set; }
    }
}
