using JuicyBurger.Services.GlobalConstants;
using System.ComponentModel.DataAnnotations;

namespace JuicyBurger.Web.InputModels.Products
{
    public class ProductTypesCreateInputModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = ServicesGlobalConstants.ModelNameErrorMessage)]
        public string Name { get; set; }
    }
}
