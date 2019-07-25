namespace JuicyBurger.Data.Models
{
    public class ProductIngredient
    {
        public string ProductId { get; set; }

        public Product Product { get; set; }

        public string IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
