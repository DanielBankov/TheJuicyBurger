using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.Models.Restaurants;
using System;

namespace JuicyBurger.Services.Restaurants
{
    public class RestaurantsService : IRestaurantsService
    {
        private readonly JuicyBurgerDbContext context;

        public RestaurantsService(JuicyBurgerDbContext context)
        {
            this.context = context;
        }

        public bool Create(RestaurantsServiceModel serviceModel)
        {
            Restaurant restaurant = AutoMapper.Mapper.Map<Restaurant>(serviceModel);

            this.context.Restaurants.Add(restaurant);
            int result = this.context.SaveChanges();

            return result > 0;
        }
    }
}
