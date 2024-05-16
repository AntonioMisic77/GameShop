using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;

namespace ProdavaonicaIgaraAPI.Repositories
{
    public class ReceiptItemRepository : IReceiptItemRepository
    {
        private readonly PIGDbContext _context;
        private readonly IGenericRepository<ReceiptItem> _genericRepository;

        public ReceiptItemRepository(IGenericRepository<ReceiptItem> genericRepository,PIGDbContext context)
        {
            _context = context;
            _genericRepository = genericRepository;
        }
        public async Task<ReceiptItem> GetAsync(int id)
        {
            return  await _context.ReceiptItems.AsNoTracking()
                                  .Include(a => a.Article)
                                  .FirstOrDefaultAsync(a => a.Id == id);
        }


        public async Task<List<ReceiptItem>> GetAllAsync()
        {
            return await _context.ReceiptItems.AsNoTracking()
                                 .Include(a => a.Article)
                                 .ToListAsync();
        }

        public async Task<List<ReceiptItem>> GetPagedReceiptItems(QueryParametars parametars)
        {
            return await _context.ReceiptItems.AsNoTracking()
                                 .Include(a => a.Article)
                                 .Skip(parametars.StartIndex)
                                 .Take(parametars.PageSize)
                                 .ToListAsync();
        }

        public async Task<int> GetReceiptItemsCount()
        {
            return await _context.ReceiptItems.AsNoTracking().CountAsync();
        }


        public async Task<ReceiptItem> CreateAsync(ReceiptItem source)
        {
            return await _genericRepository.CreateAsync(source);
        }

        public async Task<ReceiptItem> UpdateAsync(ReceiptItem source)
        {
            return await _genericRepository.UpdateAsync(source);
        }

        public async Task<ReceiptItem> DeleteAsync(int id)
        {
            return  await _genericRepository.DeleteAsync(id);
        }

        public async Task<bool> checkUniqueRecipteItem(int articleId, int recipteId)
        {
            return await _context.ReceiptItems.AnyAsync(a => a.ArticleId == articleId && a.ReceiptId == recipteId);
        }
       
    }
}
