using JuicyBurger.Services.Models.Restaurants;
using System;
using System.Collections.Generic;
using System.Text;

namespace JuicyBurger.Services.Restaurants
{
    public interface IRestaurantsService
    {
        bool Create(RestaurantsServiceModel serviceModel);
    }
}
