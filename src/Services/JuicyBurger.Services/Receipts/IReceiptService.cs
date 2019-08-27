using JuicyBurger.Services.Models.Receipts;
using System.Linq;

namespace JuicyBurger.Services.Receipts
{
    public interface IReceiptService
    {
        string Create(string recipientId);

        IQueryable<ReceiptServiceModel> GetAll();

        IQueryable<ReceiptServiceModel> GetByRecipientId(string recipientId);
    }
}
