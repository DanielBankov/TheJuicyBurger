namespace JuicyBurger.Services.Models.Orders
{
    public class OrderCartServiceModel
    {
        public string Id { get; set; }

        public string ProductImage { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal ProductPrice { get; set; }
    }
}
