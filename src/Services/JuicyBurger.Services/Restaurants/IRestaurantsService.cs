using JuicyBurger.Services.Models.Restaurants;
using System.Linq;

namespace JuicyBurger.Services.Restaurants
{
    public interface IRestaurantsService 
    {
        bool Create(RestaurantsServiceModel serviceModel);

        bool Delete(string id);

        IQueryable<RestaurantsServiceModel> AllNotDeletedRequests();
    }
}
