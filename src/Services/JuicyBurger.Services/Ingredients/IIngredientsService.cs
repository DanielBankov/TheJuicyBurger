using JuicyBurger.Data.Models;
using JuicyBurger.Services.Models.Ingredients;
using JuicyBurger.Services.Models.Products;
using System.Collections.Generic;
using System.Linq;

namespace JuicyBurger.Services.Ingredients
{
    public interface IIngredientsService
    {
        bool Create(IngredientServiceModel serviceModel);

        IQueryable<IngredientServiceModel> GetAll();

        List<string> GetAllIds(Product product);

        string SetIngredientsToProduct(Product product, List<IngredientServiceModel> ingredients);

        string IngredientsStringNames(List<string> ingredientsIds);

        List<IngredientServiceModel> MapIngNamesToIngredientServiceModel(ProductsCreateInputServiceModel serviceModel);
    }
}
