using JuicyBurger.Services.GlobalConstants;
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
        [StringLength(150, MinimumLength = 3, ErrorMessage = ServicesGlobalConstants.ModelNameErrorMessage)]
        public string Name { get; set; }

        [Required]
        [Range(0.1, 1000, ErrorMessage = ServicesGlobalConstants.WeightErrorMessage)]
        public double Weight { get; set; }

        [Required]
        [Range(0.1, 1000, ErrorMessage = ServicesGlobalConstants.CarbohydratesErrorMessage)]
        public double Carbohydrates { get; set; }

        [Required]
        [Range(0.1, 1000, ErrorMessage = ServicesGlobalConstants.ProteinsErrorMessage)]
        public double Proteins { get; set; }

        [Required]
        [Range(0.1, 1000, ErrorMessage = ServicesGlobalConstants.FatsErrorMessage)]
        public double Fat { get; set; }
    }
}
