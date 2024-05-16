using Microsoft.EntityFrameworkCore;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;

namespace ProdavaonicaIgaraAPI.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly IGenericRepository<Receipt> _genericRepository;
        private readonly PIGDbContext _context;

        public ReceiptRepository(IGenericRepository<Receipt> genericRepository, PIGDbContext context)
        {
            _genericRepository = genericRepository;
            _context = context;
        }

        public Task<Receipt> GetAsync(int id)
        {
            return _context.Receipts
                            .Include(a => a.Cashier)
                            .Include(r => r.ReceiptItems)
                            .ThenInclude(ri => ri.Article)
                            .FirstOrDefaultAsync(r => r.Id == id);
        }

        public Task<List<Receipt>> GetAllAsync()
        {
            return _genericRepository.GetAllAsync();
        }

        public async Task<List<Receipt>> GetPagedReceipts(QueryParametars parametars)
        {
            return await _context.Receipts.AsNoTracking()
                        .Include(a => a.Cashier)
                        .Include(r => r.ReceiptItems)
                        .ThenInclude(ri => ri.Article)
                        .OrderBy(r => r.Id)
                        .Skip(parametars.StartIndex)
                        .Take(parametars.PageSize)
                        .ToListAsync();
        }

        public async Task<int> GetReceiptsCount()
        {
            return await _context.Receipts.AsNoTracking().CountAsync();
        }

        public Task<Receipt> CreateAsync(Receipt source)
        {
            return _genericRepository.CreateAsync(source);
        }

        public Task<Receipt> UpdateAsync(Receipt source)
        {
            return _genericRepository.UpdateAsync(source);
        }       

        public Task<Receipt> DeleteAsync(int id)
        {
            return _genericRepository.DeleteAsync(id);
        }
    }
}
