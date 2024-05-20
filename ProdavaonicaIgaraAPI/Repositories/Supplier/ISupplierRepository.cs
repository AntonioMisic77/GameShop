using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;

namespace ProdavaonicaIgaraAPI.Repositories
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        #region methods

        Task<bool> CheckSupplierUniqueEmail(string email);

        #endregion
    }
}
