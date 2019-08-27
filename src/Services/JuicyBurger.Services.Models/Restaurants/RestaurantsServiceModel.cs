using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;

namespace JuicyBurger.Services.Models.Restaurants
{
    public class RestaurantsServiceModel : IMapFrom<Restaurant>, IMapTo<Restaurant>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }

        public string PhoneNumber { get; set; }

        public string VatNumber { get; set; }
    }
}
