using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Restaurants;
using System;
using System.Linq;

namespace JuicyBurger.Services.Restaurants
{
    public class RestaurantsService : IRestaurantsService
    {
        private readonly int num = ServicesGlobalConstants.ComparisonNumberForResultFromDbSaveChanges;

        private readonly JuicyBurgerDbContext context;

        public RestaurantsService(JuicyBurgerDbContext context)
        {
            this.context = context;
        }

        public IQueryable<RestaurantsServiceModel> AllNotDeletedRequests()
        {
            //check if some contracts are expired
            var expiredContracts = this.context.RestaurantContracts
                .Where(res => res.ExpiresOn <= DateTime.UtcNow)
                .ToList();

            foreach (var expiredContract in expiredContracts)
            {
                Restaurant restaurant = GetRestaurantById(expiredContract.Id);
                expiredContract.Restaurant.IsContractActive = false;

                this.context.RestaurantContracts.Update(expiredContract);
                this.context.SaveChanges();
            }

            var requests = this.context.Restaurants
                .Where(restaurant => restaurant.IsDeleted == false && restaurant.IsContractActive == false)
                .To<RestaurantsServiceModel>();

            return requests;
        }

        public bool CreateContract(RestaurantContractServiceModel serviceModel)
        {
            RestaurantContract restaurantContract = AutoMapper.Mapper.Map<RestaurantContract>(serviceModel);
            Restaurant restaurant = GetRestaurantById(restaurantContract.Id);
            restaurant.IsContractActive = true;

            var isContractExsited = this.context.RestaurantContracts.Any(ress => ress.Id == restaurantContract.Id);
            int result = int.MinValue;

            if (isContractExsited)
            {
                this.context.RestaurantContracts.Update(restaurantContract);
                result = this.context.SaveChanges();
                return result > num;
            }

            this.context.RestaurantContracts.Add(restaurantContract);
            result = this.context.SaveChanges();
            return result > num;
        }

        public bool CreatePartnerRequest(RestaurantsServiceModel serviceModel, string contractorId)
        {
            Restaurant restaurant = AutoMapper.Mapper.Map<Restaurant>(serviceModel);
            restaurant.IssuedOn = DateTime.UtcNow;
            restaurant.ContractorId = contractorId;

            this.context.Restaurants.Add(restaurant);
            int result = this.context.SaveChanges();

            return result > num;
        }

        public bool Delete(string id)
        {
            var restorantDb = this.context.Restaurants
                .Where(restaurant => restaurant.Id == id)
                .SingleOrDefault();

            var deletedRestaurant = restorantDb.IsDeleted = true;

            this.context.Restaurants.Update(restorantDb);
            var result = this.context.SaveChanges();

            return result > num;
        }

        private Restaurant GetRestaurantById(string id)
        {
            return this.context.Restaurants.SingleOrDefault(restaurant => restaurant.Id == id);
        }
    }
}
