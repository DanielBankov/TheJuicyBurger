﻿using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Ingredients;
using JuicyBurger.Services.Models.Products;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public bool Create(IngredientServiceModel serviceModel)
        {
            Ingredient ingredient = AutoMapper.Mapper.Map<Ingredient>(serviceModel);

            this.context.Ingredients.Add(ingredient);
            var result = this.context.SaveChanges();

            return result > num;
        }

        public IQueryable<IngredientServiceModel> GetAll()
        {
            return this.context.Ingredients.To<IngredientServiceModel>();
        }

        public List<string> GetAllIds(Product product)
        {
            return product.ProductIngredients.Select(IngId => IngId.IngredientId).ToList();
        }

        public string IngredientsStringNames(List<string> ingredientsIds)
        {
            StringBuilder sb = new StringBuilder();

            var ingredients = GetAll().ToList();

            for (int i = 0; i < ingredients.Count; i++)
            {
                if (ingredientsIds.Contains(ingredients[i].Id))
                {
                    sb.Append($"{ingredients[i].Name}{IngredientsSeparator}");
                }
            }

            string ingredientsName = sb.ToString().TrimEnd();
            ingredientsName.Remove(ingredientsName.Length - StartIndexAndCount, StartIndexAndCount);

            return ingredientsName;
        }

        public List<IngredientServiceModel> MapIngNamesToIngredientServiceModel(ProductsCreateInputServiceModel serviceModel)
        {
            List<IngredientServiceModel> ingredientsts = new List<IngredientServiceModel>();

            for (int i = 0; i < serviceModel.Ingredients.Count; i++)
            {
                var ing = new IngredientServiceModel { Name = serviceModel.Ingredients[i] };
                ingredientsts.Add(ing);
            }

            return ingredientsts;
        }

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

            return result > num;
        }

        private void SetIngredientMacronutrientsToProduct(Product product, List<Ingredient> ingredientsDb)
        {
            product.Carbohydrates = ingredientsDb.Sum(ing => (ing.Carbohydrates / For100Grams) * ing.Weight);
            product.Proteins = ingredientsDb.Sum(ing => (ing.Proteins / For100Grams) * ing.Weight);
            product.Fat = ingredientsDb.Sum(ing => (ing.Fat / For100Grams) * ing.Weight);
            product.TotalCalories = product.Carbohydrates + product.Proteins + product.Fat;
        }
    }
}
