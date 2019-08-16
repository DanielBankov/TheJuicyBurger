using System.Linq;

namespace JuicyBurger.Services
{
    public interface IProductsServices
    {
        bool Create();

        IQueryable All();
    }
}
