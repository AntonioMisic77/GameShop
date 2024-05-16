using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;

namespace ProdavaonicaIgaraAPI.Repositories
{
    public interface IReceiptItemRepository : IGenericRepository<ReceiptItem>
    {

        Task<List<ReceiptItem>> GetPagedReceiptItems(QueryParametars parametars);

        Task<int> GetReceiptItemsCount();

        Task<bool> checkUniqueRecipteItem(int articleId, int receiptId);

    }
}
