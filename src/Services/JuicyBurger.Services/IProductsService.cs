using JuicyBurger.Services.Models;
using System.Linq;

namespace JuicyBurger.Service
{
    public interface IProductsService
    {
        IQueryable<ProductAllServiceModel> All(int id);

        IQueryable<ProductAllServiceModel> Search(string searchString);

        bool Create(ProductsCreateInputServiceModel inputModel);

        bool CreateType(ProductTypeServiceModel inputModel);

        IQueryable<ProductTypeServiceModel> GetAllTypes();
    }
}
