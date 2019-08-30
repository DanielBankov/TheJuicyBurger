using AutoMapper;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Restaurants;
using JuicyBurger.Web.ViewModels.Restaurants;
using System;

namespace JuicyBurger.Web.InputModels.Restaurants
{
    public class RestaurantContractCreateInputModel 
        : IMapFrom<RestaurantContractServiceModel>, IMapTo<RestaurantContractServiceModel>
    {
        public string Id { get; set; }

        public string Conditions { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public decimal PricePerMonth { get; set; }

        public string RestaurantId { get; set; }

        public RestaurantsViewModel Restaurant { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration
        //      .CreateMap<RestaurantContractCreateInputModel, RestaurantContractServiceModel>()
        //      .ForMember(destination => destination.ProductType,
        //                    opts => opts.MapFrom(origin => new RestaurantsViewModel { Name = origin.ProductType }));
        //}
    }
}
