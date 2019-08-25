using JuicyBurger.Services.Models.Products;
using System.Linq;

namespace JuicyBurger.Service.Products
{
    public interface IProductsService
    {
        IQueryable<ProductAllServiceModel> All(int id);

        IQueryable<ProductAllServiceModel> Search(string searchString);

        ProductsDetailsServiceModel Details(string id);

        bool Create(ProductsCreateInputServiceModel inputModel);

        bool CreateType(ProductTypeServiceModel inputModel);

        IQueryable<ProductTypeServiceModel> GetAllTypes();
    }
}
