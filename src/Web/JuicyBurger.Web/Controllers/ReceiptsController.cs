using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Receipts;
using JuicyBurger.Web.ViewModels.Receipt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace JuicyBurger.Web.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly IReceiptService receiptsService;

        public ReceiptsController(IReceiptService receiptsService)
        {
            this.receiptsService = receiptsService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var receiptServiceModel = this.receiptsService.GetAll()
                .SingleOrDefault(receipt => receipt.Id == id);

            //TODO: validate if receiptServiceModel return null

            var receiptViewModel = receiptServiceModel.To<ReceiptViewModel>();

            return this.View(receiptViewModel);
        }

        [Authorize]
        public IActionResult All()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var receiptServiceModel = this.receiptsService.GetByRecipientId(userId)
                .ToList()
                .Select(receipt => receipt.To<ReceiptAllViewModel>())
                .ToList();

            return this.View(receiptServiceModel);
        }
    }
}