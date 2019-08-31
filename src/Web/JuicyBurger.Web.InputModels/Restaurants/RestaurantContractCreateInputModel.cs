using AutoMapper;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Restaurants;
using JuicyBurger.Services.ValidationAttributes;
using JuicyBurger.Web.ViewModels.Restaurants;
using System;
using System.ComponentModel.DataAnnotations;

namespace JuicyBurger.Web.InputModels.Restaurants
{
    public class RestaurantContractCreateInputModel 
        : IMapFrom<RestaurantContractServiceModel>, IMapTo<RestaurantContractServiceModel>
    {
        public string Id { get; set; }

        public string Conditions { get; set; }

        [Required]
        [DataAfterBeforeValidation("IssuedOn")]
        public DateTime ExpiresOn { get; set; }

        [Required]
        public DateTime IssuedOn { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "Price must be at least 0.01!")]
        public decimal PricePerMonth { get; set; }

        public string RestaurantId { get; set; }

        public RestaurantsViewModel Restaurant { get; set; }
    }
}
