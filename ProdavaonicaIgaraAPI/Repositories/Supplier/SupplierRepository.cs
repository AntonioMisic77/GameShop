using Microsoft.EntityFrameworkCore;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;

namespace ProdavaonicaIgaraAPI.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IGenericRepository<Supplier> _genericRepository;
        private readonly PIGDbContext _context;

        public SupplierRepository(IGenericRepository<Supplier> genericRepository, PIGDbContext context) 
        { 
            _genericRepository = genericRepository;
            _context = context;
        }
         public Task<Supplier> GetAsync(int id)
        {
            return _genericRepository.GetAsync(id);
        }

        public Task<List<Supplier>> GetAllAsync()
        {
            return _genericRepository.GetAllAsync();
        }

        public Task<Supplier> CreateAsync(Supplier source)
        {
            return _genericRepository.CreateAsync(source);
        }

        public Task<Supplier> UpdateAsync(Supplier source)
        {
            return _genericRepository.UpdateAsync(source);
        }

        public Task<Supplier> DeleteAsync(int id)
        {
            return _genericRepository.DeleteAsync(id);
        }

        public Task<bool> CheckSupplierUniqueEmail(string email)
        {
            return _context.Suppliers.AnyAsync(x => x.Email == email);
        }

       
    }
}
