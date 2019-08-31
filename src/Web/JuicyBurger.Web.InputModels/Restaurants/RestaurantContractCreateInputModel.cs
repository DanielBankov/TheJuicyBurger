using JuicyBurger.Services.GlobalConstants;
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
        [DataAfterBeforeValidation(ServicesGlobalConstants.CustomValidationDateBefore)]
        public DateTime ExpiresOn { get; set; }

        [Required]
        public DateTime IssuedOn { get; set; }

        [Required]
        [Range(typeof(decimal), ServicesGlobalConstants.DecimalMinValue, ServicesGlobalConstants.DecimalMaxValue, ErrorMessage = ServicesGlobalConstants.ProductsCreateInputModelPriceErrorMessage)]
        public decimal PricePerMonth { get; set; }

        public string RestaurantId { get; set; }

        public RestaurantsViewModel Restaurant { get; set; }
    }
}
