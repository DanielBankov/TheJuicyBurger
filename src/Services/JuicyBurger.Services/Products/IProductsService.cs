using JuicyBurger.Services.Models.Products;
using System.Linq;
using System.Text;

namespace JuicyBurger.Service.Products
{
    public interface IProductsService
    {
        IQueryable<ProductServiceModel> All(int id);

        IQueryable<ProductServiceModel> Search(string searchString);

        ProductsDetailsServiceModel Details(string id);

        string Create(ProductServiceModel inputModel);

        bool CreateType(ProductTypeServiceModel inputModel);

        IQueryable<ProductTypeServiceModel> GetAllTypes();

        string GetAllIngredientsName(ProductsDetailsServiceModel serviceModel);
    }
}
