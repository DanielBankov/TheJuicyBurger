using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Models.Receipts;
using JuicyBurger.Services.Receipts;
using JuicyBurger.Web.ViewModels.Receipt;
using Microsoft.AspNetCore.Mvc;

namespace JuicyBurger.Web.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly IReceiptService receiptsService;

        public ReceiptsController(IReceiptService receiptsService)
        {
            this.receiptsService = receiptsService;
        }


        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Details(string id)
        {
            var receiptServiceModel = this.receiptsService.GetAll()
                .SingleOrDefault(receipt => receipt.Id == id);

            //TODO: validate if receiptServiceModel return null

            var receiptViewModel = receiptServiceModel.To<ReceiptViewModel>();

            return this.View(receiptViewModel);
        }

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