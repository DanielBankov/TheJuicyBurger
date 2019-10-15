using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Ingredients;
using JuicyBurger.Services.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuicyBurger.Services.Ingredients
{
    public class IngredientsService : IIngredientsService
    {
        private readonly int num = ServicesGlobalConstants.ComparisonNumberForResultFromDbSaveChanges;
        private const string IngredientsSeparator = ", ";
        private const int StartIndexAndCount = 1;
        private const double For100Grams = 100;

        private readonly JuicyBurgerDbContext context;

        public IngredientsService(JuicyBurgerDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(IngredientServiceModel serviceModel)
        {
            Ingredient ingredient = AutoMapper.Mapper.Map<Ingredient>(serviceModel);

            await this.context.Ingredients.AddAsync(ingredient);
            var result = await this.context.SaveChangesAsync();

            return result > num;
        }

        public IQueryable<IngredientServiceModel> GetAll()
        {
            return this.context.Ingredients.To<IngredientServiceModel>();
        }

        public async Task<List<string>> GetAllIds(Product product)
        {
            return await Task.Run(() => product.ProductIngredients.Select(IngId => IngId.IngredientId).ToList());
        }

        public async Task<string> IngredientsStringNames(List<string> ingredientsIds)
        {
            StringBuilder sb = new StringBuilder();

            var ingredients = await GetAll().ToListAsync();

            for (int i = 0; i < ingredients.Count; i++)
            {
                if (ingredientsIds.Contains(ingredients[i].Id))
                {
                    sb.Append($"{ingredients[i].Name}{IngredientsSeparator}");
                }
            }

            string ingredientsName = sb.ToString().TrimEnd();
            ingredientsName = ingredientsName.Remove(ingredientsName.Length - StartIndexAndCount, StartIndexAndCount);

            return ingredientsName;
        }

        public async Task<List<IngredientServiceModel>> MapIngNamesToIngredientServiceModel(ProductsCreateInputServiceModel serviceModel)
        {
            List<IngredientServiceModel> ingredientsts = new List<IngredientServiceModel>();

            for (int i = 0; i < serviceModel.Ingredients.Count; i++)
            {
                var ing = new IngredientServiceModel { Name = serviceModel.Ingredients[i] };
                ingredientsts.Add(ing);
            }

            return ingredientsts;
        }

        public async Task<bool> SetIngredientsToProduct(Product product, List<IngredientServiceModel> ingredients)
        {
            List<string> allIngredientsNames = new List<string>();
            await Task.Run(() => allIngredientsNames = ingredients.Select(ingredient => ingredient.Name).ToList());

            List<Ingredient> ingredientsDb = await this.context.Ingredients
                .Where(ingredient => allIngredientsNames.Contains(ingredient.Name))
                .ToListAsync();

            await SetIngredientMacronutrientsToProduct(product, ingredientsDb);

            for (int i = 0; i < ingredientsDb.Count; i++)
            {
                ProductIngredient productIngredient = new ProductIngredient
                {
                    Product = product,
                    Ingredient = ingredientsDb[i]
                };

                await this.context.ProductIngredients.AddAsync(productIngredient);
            }

            var result = await this.context.SaveChangesAsync();

            return result > num;
        }

        private Task SetIngredientMacronutrientsToProduct(Product product, List<Ingredient> ingredientsDb)
        {
            product.Carbohydrates = ingredientsDb.Sum(ing => (ing.Carbohydrates / For100Grams) * ing.Weight);
            product.Proteins = ingredientsDb.Sum(ing => (ing.Proteins / For100Grams) * ing.Weight);
            product.Fat = ingredientsDb.Sum(ing => (ing.Fat / For100Grams) * ing.Weight);
            product.TotalCalories = product.Carbohydrates + product.Proteins + product.Fat;

            return Task.CompletedTask;
        }
    }
}
