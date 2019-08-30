namespace JuicyBurger.Web.ViewModels.Products
{
    public class ProductsCreateViewModel // del this
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int ProductTypeId { get; set; }

        public ProductTypeViewModel ProductType { get; set; }

       //public ICollection</*Ingredient*/> Ingredients { get; set; }
       
       //public ICollection<Review> Reviews { get; set; }
    }
}
