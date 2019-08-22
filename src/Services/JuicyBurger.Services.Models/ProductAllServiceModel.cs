namespace JuicyBurger.Services.Models
{
    public class ProductAllServiceModel
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Weight { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int ProductTypeId { get; set; }

        public string ProductType { get; set; }
    }
}
