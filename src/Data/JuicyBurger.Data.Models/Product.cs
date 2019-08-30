using System.Collections.Generic;

namespace JuicyBurger.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.ProductIngredients = new List<ProductIngredient>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Carbohydrates { get; set; }

        public double Fat { get; set; }

        public double Proteins { get; set; }

        public double TotalCalories { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }

        public virtual List<ProductIngredient> ProductIngredients { get; set; }

        //public ICollection<Review> Reviews { get; set; }
    }
}
