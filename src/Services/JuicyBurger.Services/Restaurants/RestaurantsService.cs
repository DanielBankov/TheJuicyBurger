using JuicyBurger.Data;
using JuicyBurger.Data.Models;
using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Restaurants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IQueryable<RestaurantsServiceModel>> AllNotDeletedRequests()
        {
            //check if some contracts are expired
            var expiredContracts = await this.context.RestaurantContracts
                .Where(res => res.ExpiresOn <= DateTime.UtcNow)
                .ToListAsync();

            foreach (var expiredContract in expiredContracts)
            {
                Restaurant restaurant = await GetRestaurantById(expiredContract.Id);
                expiredContract.Restaurant.IsContractActive = false;

                await Task.Run(() => this.context.RestaurantContracts.Update(expiredContract));
                await this.context.SaveChangesAsync();
            }

            var requests = this.context.Restaurants
                .Where(restaurant => restaurant.IsDeleted == false && restaurant.IsContractActive == false)
                .To<RestaurantsServiceModel>();

            return requests;
        }

        public async Task<bool> CreateContract(RestaurantContractServiceModel serviceModel)
        {
            RestaurantContract restaurantContract = AutoMapper.Mapper.Map<RestaurantContract>(serviceModel);
            Restaurant restaurant = await GetRestaurantById(restaurantContract.Id);
            restaurant.IsContractActive = true;

            var isContractExsited = await this.context.RestaurantContracts
                .AnyAsync(ress => ress.Id == restaurantContract.Id);
            int result = int.MinValue;

            if (isContractExsited)
            {
                await Task.Run(() => this.context.RestaurantContracts.Update(restaurantContract));
                result = await this.context.SaveChangesAsync();
                return result > num;
            }

            await this.context.RestaurantContracts.AddAsync(restaurantContract);
            result = await this.context.SaveChangesAsync();
            return result > num;
        }

        public async Task<bool> CreatePartnerRequest(RestaurantsServiceModel serviceModel, string contractorId)
        {
            Restaurant restaurant = AutoMapper.Mapper.Map<Restaurant>(serviceModel);
            restaurant.IssuedOn = DateTime.UtcNow;
            restaurant.ContractorId = contractorId;

            await this.context.Restaurants.AddAsync(restaurant);
            int result = await this.context.SaveChangesAsync();

            return result > num;
        }

        public async Task<bool> Delete(string id)
        {
            var restorantDb = await this.context.Restaurants
                .Where(restaurant => restaurant.Id == id)
                .SingleOrDefaultAsync();

            var deletedRestaurant = restorantDb.IsDeleted = true;

            await Task.Run(() => this.context.Restaurants.Update(restorantDb));
            var result = await this.context.SaveChangesAsync();

            return result > num;
        }

        private async Task<Restaurant> GetRestaurantById(string id)
        {
            return await this.context.Restaurants.SingleOrDefaultAsync(restaurant => restaurant.Id == id);
        }
    }
}
