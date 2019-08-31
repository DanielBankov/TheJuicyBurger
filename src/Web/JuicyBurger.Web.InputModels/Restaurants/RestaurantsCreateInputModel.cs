using JuicyBurger.Services.GlobalConstants;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Restaurants;
using JuicyBurger.Web.ViewModels.Users;
using System.ComponentModel.DataAnnotations;

namespace JuicyBurger.Web.InputModels.Restaurants
{
    public class RestaurantsCreateInputModel : IMapFrom<RestaurantsServiceModel>, IMapTo<RestaurantsServiceModel>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = ServicesGlobalConstants.ModelNameErrorMessage)]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(ServicesGlobalConstants.PhoneNumberRegex, ErrorMessage = ServicesGlobalConstants.RestaurantsCreateInputModelPhoneErrorMessage)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(ServicesGlobalConstants.RestaurantsCreateInputModelCompanyMaxLenght, 
            MinimumLength = ServicesGlobalConstants.RestaurantsCreateInputModelCompanyMinLenght,
            ErrorMessage = ServicesGlobalConstants.RestaurantsCreateInputModelCompanyErrorMessage)]
        public string Company { get; set; }

        [Required]
        [StringLength(ServicesGlobalConstants.RestaurantsCreateInputModelLocationMaxLenght,
            MinimumLength = ServicesGlobalConstants.RestaurantsCreateInputModelLocationMinLenght,
            ErrorMessage = ServicesGlobalConstants.RestaurantsCreateInputModelLocationErrorMessage)]
        public string Location { get; set; }

        [Required]
        [StringLength(ServicesGlobalConstants.RestaurantsCreateInputModelVATMaxLenght,
            MinimumLength = ServicesGlobalConstants.RestaurantsCreateInputModelVATMinLenght,
            ErrorMessage = ServicesGlobalConstants.RestaurantsCreateInputModelVATErrorMessage)]
        public string VatNumber { get; set; }

        public string ContractorId { get; set; }

        public JBUserViewModel Contractor { get; set; }
    }
}
