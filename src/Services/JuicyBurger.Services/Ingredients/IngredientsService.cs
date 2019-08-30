using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Ingredients;
using JuicyBurger.Services.Models.Products;
using System.Collections.Generic;
using System.Linq;

namespace JuicyBurger.Services.Ingredients
{
    public class IngredientsService : IIngredientsService
    {
        private readonly JuicyBurgerDbContext context;

        public IngredientsService(JuicyBurgerDbContext context)
        {
            this.context = context;
        }

        public bool Create(IngredientServiceModel serviceModel)
        {
            Ingredient ingredient = AutoMapper.Mapper.Map<Ingredient>(serviceModel);

            this.context.Ingredients.Add(ingredient);
            var result = this.context.SaveChanges();

            return result > 0;
        }

        public IQueryable<IngredientServiceModel> GetAll()
        {
            return this.context.Ingredients.To<IngredientServiceModel>();
        }

        public List<string> GetAllIds(Product product)
        {
            return product.ProductIngredients.Select(IngId => IngId.IngredientId).ToList();
        }

        //public List<IngredientServiceModel> MapIngNamesToIngredientServiceModel(ProductServiceModel serviceModel)
        //{
        //    ////var test = serviceModel.Ingredients.ToList();
        //    //List<IngredientServiceModel> inList = new List<IngredientServiceModel>();

        //    //for (int i = 0; i < serviceModel.Ingredients.Count; i++)
        //    //{
        //    //    var ing = new IngredientServiceModel { Name = serviceModel.Ingredients[i] };
        //    //    inList.Add(ing);
        //    //}

        //    //return inList;
        //}

        public bool SetIngredientsToProduct(Product product, List<IngredientServiceModel> ingredients)
        {
            List<string> allIngredientsNames = new List<string>();
            allIngredientsNames = ingredients.Select(ingredient => ingredient.Name).ToList();

            List<Ingredient> ingredientsDb = this.context.Ingredients
                .Where(ingredient => allIngredientsNames.Contains(ingredient.Name))
                .ToList();

            SetIngredientMacronutrientsToProduct(product, ingredientsDb);

            for (int i = 0; i < ingredientsDb.Count; i++)
            {
                ProductIngredient productIngredient = new ProductIngredient
                {
                    Product = product,
                    Ingredient = ingredientsDb[i]
                };

                this.context.ProductIngredients.Add(productIngredient);
            }

            var result = this.context.SaveChanges();
            return result > 0;
        }

        private void SetIngredientMacronutrientsToProduct(Product product, List<Ingredient> ingredientsDb)
        {
            product.Carbohydrates = ingredientsDb.Sum(ing => (ing.Carbohydrates / 100) * ing.Weight);
            product.Proteins = ingredientsDb.Sum(ing => (ing.Proteins / 100) * ing.Weight);
            product.Fat = ingredientsDb.Sum(ing => (ing.Fat / 100) * ing.Weight);
            product.TotalCalories = product.Carbohydrates + product.Proteins + product.Fat;
        }
    }
}
