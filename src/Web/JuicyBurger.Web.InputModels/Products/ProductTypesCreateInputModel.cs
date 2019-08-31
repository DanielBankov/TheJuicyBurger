using System.ComponentModel.DataAnnotations;

namespace JuicyBurger.Web.InputModels.Products
{
    public class ProductTypesCreateInputModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Name must contains at least 3 symbols and max 150!")]
        public string Name { get; set; }
    }
}
