using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Ingredients;
using System.ComponentModel.DataAnnotations;

namespace JuicyBurger.Web.InputModels.Ingredients
{
    public class IngredientsCreateInputModel : IMapFrom<IngredientServiceModel>, IMapTo<IngredientServiceModel>
    {
        //add constants
        public string Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Name must contains at least 3 symbols and max 150!")]
        public string Name { get; set; }

        [Required]
        [Range(0.1, 1000, ErrorMessage = "Weight must be between 0.01g and 1000g")]
        public double Weight { get; set; }

        [Required]
        [Range(0.1, 1000, ErrorMessage = "Carbohydrates must be between 0.01g and 1000g")]
        public double Carbohydrates { get; set; }

        [Required]
        [Range(0.1, 1000, ErrorMessage = "Proteins must be between 0.01g and 1000g")]
        public double Proteins { get; set; }

        [Required]
        [Range(0.1, 1000, ErrorMessage = "Fat must be between 0.01g and 1000g")]
        public double Fat { get; set; }
    }
}
