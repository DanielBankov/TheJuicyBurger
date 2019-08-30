using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;

namespace JuicyBurger.Services.Models.Ingredients
{
    public class IngredientServiceModel : IMapFrom<Ingredient>, IMapTo<Ingredient>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public double Carbohydrates { get; set; }

        public double Proteins { get; set; }

        public double Fat { get; set; }
    }
}
