using JuicyBurger.Services.Models.Restaurants;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyBurger.Services.Restaurants
{
    public interface IRestaurantsService 
    {
        Task<bool> CreatePartnerRequest(RestaurantsServiceModel serviceModel, string contractorId);

        Task<bool> CreateContract(RestaurantContractServiceModel serviceModel);

        Task<bool> Delete(string id);

        Task<IQueryable<RestaurantsServiceModel>> AllNotDeletedRequests();
    }
}
