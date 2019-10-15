using JuicyBurger.Data.Models;
using JuicyBurger.Services.Models.Ingredients;
using JuicyBurger.Services.Models.Products;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyBurger.Services.Ingredients
{
    public interface IIngredientsService
    {
        Task<bool> Create(IngredientServiceModel serviceModel);

        IQueryable<IngredientServiceModel> GetAll();

        Task<List<string>> GetAllIds(Product product);

        Task<bool> SetIngredientsToProduct(Product product, List<IngredientServiceModel> ingredients);

        Task<string> IngredientsStringNames(List<string> ingredientsIds);

        Task<List<IngredientServiceModel>> MapIngNamesToIngredientServiceModel(ProductsCreateInputServiceModel serviceModel);
    }
}
