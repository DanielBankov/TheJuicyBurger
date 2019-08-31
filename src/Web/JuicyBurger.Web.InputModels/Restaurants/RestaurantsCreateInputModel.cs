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
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Name must contains at least 3 symbols and max 150!")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Invalid phone number!")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Company must contains at least 3 symbols and max 150!")]
        public string Company { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Location must contains at least 3 symbols and max 250!")]
        public string Location { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "VatNumber must contains at least 9 numbers and max 20!")]
        public string VatNumber { get; set; }

        public string ContractorId { get; set; }

        public JBUserViewModel Contractor { get; set; }
    }
}
