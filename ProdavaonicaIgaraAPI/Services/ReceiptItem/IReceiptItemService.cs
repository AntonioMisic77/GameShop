using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Data.ReceiptItem;

namespace ProdavaonicaIgaraAPI.Services.ReceiptItem
{
    public interface IReceiptItemService
    {
        Task<ReceiptItemDto> GetReceiptItemAsync(int id);

        Task<List<ReceiptItemDto>> GetReceiptItemsAsync();

        Task<PagedResult<ReceiptItemDto>> GetReceiptItemsAsync(QueryParametars parametars);

        Task<ReceiptItemDto> CreateReceiptItemAsync(ReceiptItemDto receiptItemDto);

        Task<ReceiptItemDto> UpdateReceiptItemAsync(ReceiptItemDto receiptItemDto);

        Task<ReceiptItemDto> DeleteReceiptItem(int id);
    }
}
