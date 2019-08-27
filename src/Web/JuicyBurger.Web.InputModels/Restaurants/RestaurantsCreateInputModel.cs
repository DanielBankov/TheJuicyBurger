using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Restaurants;

namespace JuicyBurger.Web.InputModels.Restaurants
{
    public class RestaurantsCreateInputModel : IMapFrom<RestaurantsServiceModel>, IMapTo<RestaurantsServiceModel>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }

        public string VatNumber { get; set; }
    }
}
