using JuicyBurger.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace JuicyBurger.Service
{
    public interface IProductsService
    {
        IQueryable All();

        bool Create(ProductsCreateInputServiceModel inputModel);

        bool CreateType(ProductTypeServiceModel inputModel);

        IQueryable<ProductTypeServiceModel> GetAllTypes();
    }
}
