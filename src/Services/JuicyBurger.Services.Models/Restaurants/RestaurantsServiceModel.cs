using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using System;

namespace JuicyBurger.Services.Models.Restaurants
{
    public class RestaurantsServiceModel : IMapFrom<Restaurant>, IMapTo<Restaurant>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Company { get; set; }

        public DateTime IssuedOn { get; set; }

        public bool IsContractActive { get; set; }

        public bool IsDeleted { get; set; }

        public string Location { get; set; }

        public string PhoneNumber { get; set; }

        public string VatNumber { get; set; }
    }
}
