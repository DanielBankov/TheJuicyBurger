using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Restaurants;
using System;

namespace JuicyBurger.Web.ViewModels.Restaurants
{
    public class RestaurantsViewModel : IMapFrom<RestaurantsServiceModel>
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public DateTime IssuedOn { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsContractActive { get; set; }

        public string PhoneNumber { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }

        public string VatNumber { get; set; }
    }
}
