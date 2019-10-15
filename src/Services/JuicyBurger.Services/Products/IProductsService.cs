using JuicyBurger.Services.Models.Products;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyBurger.Service.Products
{
    public interface IProductsService
    {
        IQueryable<ProductServiceModel> AllByProductTypeId(int? id);

        IQueryable<ProductServiceModel> Search(string searchString);

        Task<ProductsDetailsServiceModel> Details(string id);

        Task<bool> Delete(string id);

        Task<bool> Create(ProductServiceModel inputModel);

        Task<bool> CreateType(ProductTypeServiceModel inputModel);

        IQueryable<ProductTypeServiceModel> GetAllTypes();

        IQueryable<ProductsAllServiceModel> GetAll();

        Task<string> GetAllIngredientsName(ProductsDetailsServiceModel serviceModel);
    }
}
