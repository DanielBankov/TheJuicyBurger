using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Ingredients;

namespace JuicyBurger.Web.InputModels.Ingredients
{
    public class IngredientsCreateInputModel : IMapFrom<IngredientServiceModel>, IMapTo<IngredientServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public double Carbohydrates { get; set; }

        public double Proteins { get; set; }

        public double Fat { get; set; }
    }
}
