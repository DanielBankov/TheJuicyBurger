using JuicyBurger.Services.Models.Products;
using System.Linq;

namespace JuicyBurger.Service.Products
{
    public interface IProductsService
    {
        IQueryable<ProductServiceModel> AllByProductTypeId(int id);

        IQueryable<ProductServiceModel> Search(string searchString);

        ProductsDetailsServiceModel Details(string id);
        bool Delete(string id);

        bool Create(ProductServiceModel inputModel);

        bool CreateType(ProductTypeServiceModel inputModel);

        IQueryable<ProductTypeServiceModel> GetAllTypes();

        IQueryable<ProductsAllServiceModel> GetAll();


        string GetAllIngredientsName(ProductsDetailsServiceModel serviceModel);
    }
}
