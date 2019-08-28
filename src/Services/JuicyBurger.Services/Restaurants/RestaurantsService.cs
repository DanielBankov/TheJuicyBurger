using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Restaurants;
using System;
using System.Linq;

namespace JuicyBurger.Services.Restaurants
{
    public class RestaurantsService : IRestaurantsService
    {
        private readonly JuicyBurgerDbContext context;

        public RestaurantsService(JuicyBurgerDbContext context)
        {
            this.context = context;
        }

        public IQueryable<RestaurantsServiceModel> AllNotDeletedRequests()
        {
            return this.context.Restaurants
                .Where(restaurant => restaurant.IsDeleted == false)
                .To<RestaurantsServiceModel>();
        }

        public bool Create(RestaurantsServiceModel serviceModel)
        {
            Restaurant restaurant = AutoMapper.Mapper.Map<Restaurant>(serviceModel);
            restaurant.IssuedOn = DateTime.UtcNow;

            this.context.Restaurants.Add(restaurant);
            int result = this.context.SaveChanges();

            return result > 0;
        }

        public bool Delete(string id)
        {
            var restorantDb = this.context.Restaurants
                .Where(restaurant => restaurant.Id == id)
                .SingleOrDefault();

            var deletedRestaurant = restorantDb.IsDeleted = true;

            this.context.Restaurants.Update(restorantDb);
            var result = this.context.SaveChanges();

            return result > 0;
        }
    }
}
