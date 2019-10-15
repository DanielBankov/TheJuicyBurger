using JuicyBurger.Services.Models.Receipts;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyBurger.Services.Receipts
{
    public interface IReceiptService
    {
        Task<string> Create(string recipientId);

        IQueryable<ReceiptServiceModel> GetAll();

        IQueryable<ReceiptServiceModel> GetByRecipientId(string recipientId);
    }
}
