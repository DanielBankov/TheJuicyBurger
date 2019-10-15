using JuicyBurger.Services.Mapping;
using JuicyBurger.Services.Receipts;
using JuicyBurger.Web.ViewModels.Receipt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            var receiptServiceModel = await this.receiptsService.GetAll()
                .SingleOrDefaultAsync(receipt => receipt.Id == id);

            //TODO: validate if receiptServiceModel return null

            var receiptViewModel = receiptServiceModel.To<ReceiptViewModel>();

            return this.View(receiptViewModel);
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var userId = await Task.Run(() => this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var receiptsFromDb = await this.receiptsService.GetByRecipientId(userId)
                .ToListAsync();

            var receiptServiceModel = await Task.Run(() => receiptsFromDb
            .Select(receipt => receipt.To<ReceiptAllViewModel>())
            .ToList());

            return this.View(receiptServiceModel);
        }
    }
}