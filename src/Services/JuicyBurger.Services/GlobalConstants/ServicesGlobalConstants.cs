namespace JuicyBurger.Services.GlobalConstants
{
    public static class ServicesGlobalConstants
    {
        public const int ComparisonNumberForResultFromDbSaveChanges = 0;

        public const string DateTimeFormat = "dd/MM/yyyy";

        public const string PriceRouting = "F2";

        public const string DecimalMaxValue = "79228162514264337593543950335";

        public const string DecimalMinValue = "0.01";

        public const string CustomValidationDateBefore = "IssuedOn";

        public const string PhoneNumberRegex = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";




        //ViewData
        public const string IngredientsViewData = "ingredients";

        public const string ProductTypeViewData = "productTypes";

        public const string ProductTypesMenuViewData = "productTypesMenu";

        public const string IngredientsNameViewData = "ingredientsName";

        
        public const string Admin = "Admin";

        public const string AdminUpperCase = "ADMIN";

        public const string User = "User";

        public const string UserUpperCase = "USER";


        public const string Administration = "Administration";

        //HomeController
        public const string HomeIndex = "/";

        //ProductController
        public const string TypeCreateRoute = "/Administration/Products/Type/Create";

        public const string ReturnTypeCreateView = "Type/Create";

        public const string RedirectProductCreate = "/Administration/Products/Create";

        public const string HttpProductsAllId = "/Products/All/{id?}";
            
        public const string HttpProductsDetailsId = "/Products/Details/{id}";

        public const string ViewProductsAll = "All";


        //OrdersController
        public const string RedirectReceiptsDetails = "/Receipts/Details/";

        //RestaurantController
        public const string RedirectRestaurantRequest = "/Administration/Restaurants/Requests";

        public const string ViewRestaurantsContracts = "/Administration/Restaurants/Contracts";

        public const string ViewContractsCreate = "Contracts/Create";

        public const string HttpRestaurantsContractsCreateId = "/Administration/Restaurants/Contracts/Create/{id}";


        public const string NoActiveOrdersExceptionMessage = "No active orders!";

        public const string ProductNameExistsExceptionMessage = "Product with this ({0}) name already exists!";

        public const string OrderStatusFinish = "Finished";

        public const string OrderStatusActive = "Active";

        //Models
        public const string ModelNameErrorMessage = "Name must contains at least 3 symbols and max 150!";

        public const string WeightErrorMessage = "Weight must be between 0.01g and 1000g";

        public const string CarbohydratesErrorMessage = "Carbohydrates must be between 0.01g and 1000g";

        public const string ProteinsErrorMessage = "Proteins must be between 0.01g and 1000g";

        public const string FatsErrorMessage = "Fat must be between 0.01g and 1000g";

        public const string ProductsCreateInputModelPriceErrorMessage = "Price must be at least 0.01!";

        public const string ProductsCreateInputModelProductTypeErrorMessage = "Product Type must be chosen!";

        public const string RestaurantsCreateInputModelPhoneErrorMessage = "Invalid phone number!";

        public const int RestaurantsCreateInputModelCompanyMaxLenght = 150;

        public const int RestaurantsCreateInputModelCompanyMinLenght = 3;

        public const string RestaurantsCreateInputModelCompanyErrorMessage = "Company must contains at least 3 symbols and max 150!";

        public const int RestaurantsCreateInputModelLocationMaxLenght = 250;

        public const int RestaurantsCreateInputModelLocationMinLenght = 3;

        public const string RestaurantsCreateInputModelLocationErrorMessage = "Location must contains at least 3 symbols and max 250!";

        public const int RestaurantsCreateInputModelVATMaxLenght = 20;

        public const int RestaurantsCreateInputModelVATMinLenght = 9;

        public const string RestaurantsCreateInputModelVATErrorMessage = "VatNumber must contains at least 9 numbers and max 20!";
    }
}
