using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using System;

namespace JuicyBurger.Services.Models.Restaurants
{
    public class RestaurantContractServiceModel : IMapFrom<RestaurantContract>, IMapTo<RestaurantContract>
    {
        public string Id { get; set; }

        public string Conditions { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public decimal PricePerMonth { get; set; }

        public string RestaurantId { get; set; }

        public RestaurantsServiceModel Restaurant { get; set; }
    }
}
