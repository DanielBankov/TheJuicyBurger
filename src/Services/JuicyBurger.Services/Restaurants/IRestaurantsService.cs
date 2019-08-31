using JuicyBurger.Services.Models.Restaurants;
using System.Linq;

namespace JuicyBurger.Services.Restaurants
{
    public interface IRestaurantsService 
    {
        bool CreatePartnerRequest(RestaurantsServiceModel serviceModel, string contractorId);

        bool CreateContract(RestaurantContractServiceModel serviceModel);

        bool Delete(string id);

        IQueryable<RestaurantsServiceModel> AllNotDeletedRequests();
    }
}
