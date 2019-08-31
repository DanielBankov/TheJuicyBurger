using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Ingredients;
using JuicyBurger.Services.Models.Ingredients;
using JuicyBurger.Web.InputModels.Ingredients;
using Microsoft.AspNetCore.Mvc;

namespace JuicyBurger.Web.Areas.Administration.Controllers
{
    public class IngredientsController : AdminController
    {
        private readonly IIngredientsService ingredientsService;

        public IngredientsController(IIngredientsService ingredientsService)
        {
            this.ingredientsService = ingredientsService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(IngredientsCreateInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var ingredient = AutoMapper.Mapper.Map<IngredientServiceModel>(inputModel);
            this.ingredientsService.Create(ingredient);

            return this.Redirect(ServicesGlobalConstants.HomeIndex); // redirect to ingredients all
        }
    }
}