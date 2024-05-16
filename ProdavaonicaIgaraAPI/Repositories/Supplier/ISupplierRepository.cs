using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;

namespace ProdavaonicaIgaraAPI.Repositories
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<bool> CheckSupplierUniqueEmail(string email);

    }
}
