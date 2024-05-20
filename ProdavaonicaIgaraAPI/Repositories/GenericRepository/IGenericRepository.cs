using ProdavaonicaIgaraAPI.Data.Pageing;

namespace ProdavaonicaIgaraAPI.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        #region methods

        Task<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> CreateAsync(T source);
        Task<T> UpdateAsync(T source);
        Task<T> DeleteAsync(int id);

        #endregion
    }
}
