using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Ingredients;

namespace JuicyBurger.Web.ViewModels.Ingredients
{
    public class IngredientsAllCreateViewModel : IMapFrom<IngredientServiceModel>, IMapTo<IngredientServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
