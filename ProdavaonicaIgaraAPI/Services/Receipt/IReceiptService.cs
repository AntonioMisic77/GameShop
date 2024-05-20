using ProdavaonicaIgaraAPI.Data.Articles;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Data.Receipt;

namespace ProdavaonicaIgaraAPI.Services.Receipt
{
    public interface IReceiptService
    {
        #region methods
        Task<ReceiptDto> GetReceiptAsync(int id);

        Task<PagedResult<ReceiptDto>> GetReceiptsAsync(QueryParametars parametars);

        Task<ReceiptDto> CreateReceiptAsync(ReceiptDto articleDto);

        Task<ReceiptDto> UpdateReceiptAsync(ReceiptDto articleDto);

        Task<ReceiptDto> DeleteReceipt(int id);

        #endregion
    }
}
