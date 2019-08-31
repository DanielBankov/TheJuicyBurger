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
            return View();
        }

        [HttpPost]
        public IActionResult Create(IngredientsCreateInputModel inputModel)
        {
            var ingredient = AutoMapper.Mapper.Map<IngredientServiceModel>(inputModel);
            this.ingredientsService.Create(ingredient);

            return Redirect("/"); // redirect to ingredients all
        }
    }
}