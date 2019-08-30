using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Ingredients;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuicyBurger.Web.ViewModels.Ingredients
{
    public class IngredientsProductViewModel : IMapFrom<IngredientServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public double Carbohydrates { get; set; }

        public double Proteins { get; set; }

        public double Fat { get; set; }
    }
}
