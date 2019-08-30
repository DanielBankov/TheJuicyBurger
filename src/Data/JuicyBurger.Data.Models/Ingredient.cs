using System.Collections.Generic;

namespace JuicyBurger.Data.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            this.ProductIngredients = new List<ProductIngredient>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public double Carbohydrates { get; set; }

        public double Proteins { get; set; }

        public double Fat { get; set; }

        public virtual List<ProductIngredient> ProductIngredients { get; set; }
    }
}