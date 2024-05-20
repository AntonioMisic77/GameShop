using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;

namespace ProdavaonicaIgaraAPI.Repositories
{
    public interface IReceiptRepository : IGenericRepository<Receipt>
    {
        #region methods

        Task<List<Receipt>> GetPagedReceipts(QueryParametars parametars);

        Task<int> GetReceiptsCount();

        #endregion
    }
}
