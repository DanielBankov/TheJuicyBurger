using System.Collections.Generic;

namespace JuicyBurger.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.Ingredients = new HashSet<Ingredient>();
            this.Reviews = new HashSet<Review>();
            this.Orders = new HashSet<Order>();
        }

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

        public ProductType ProductType { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
