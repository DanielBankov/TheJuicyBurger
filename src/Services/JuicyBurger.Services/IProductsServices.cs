using JuicyBurger.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace JuicyBurger.Services
{
    public interface IProductsServices
    {
        IQueryable All();

        bool Create(ProductsCreateInputServiceModel inputModel);

        bool CreateType(ProductTypeServiceModel inputModel);

        IQueryable<ProductTypeServiceModel> GetAllTypes();
    }
}
