using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;

namespace ProdavaonicaIgaraAPI.Repositories
{
    public interface IArticleRepository : IGenericRepository<Article>
    {
        #region methods
        Task<List<Article>> GetPagedAsync(QueryParametars parametars);

        Task<int> GetArtilceCount(string filterText);

        #endregion
    }
}
