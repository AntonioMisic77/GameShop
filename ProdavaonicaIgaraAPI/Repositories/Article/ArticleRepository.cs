using Microsoft.EntityFrameworkCore;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;

namespace ProdavaonicaIgaraAPI.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        #region properties
        private readonly IGenericRepository<Article> _genericRepository;
        private readonly PIGDbContext _context;
        #endregion

        #region ctor
        public ArticleRepository(IGenericRepository<Article> genericRepository, PIGDbContext context)
        {
            _genericRepository = genericRepository;
            _context = context;
        }
        #endregion

        #region methods
        public Task<Article> GetAsync(int id)
        {
            return _context.Articles.AsNoTracking().Include(a => a.Supplier).FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<List<Article>> GetAllAsync()
        {
            return _genericRepository.GetAllAsync();
        }

        public async Task<List<Article>> GetPagedAsync(QueryParametars parametars)
        {
            var filterText = parametars.filterText.ToLower().Trim();

            return await _context.Articles.AsNoTracking()
                        .Include(a => a.Supplier)
                        .Where(a => a.Name.ToLower().Contains(filterText) || filterText.Trim().Equals(String.Empty))
                        .OrderBy(a => a.Name)
                        .Skip(parametars.StartIndex)
                        .Take(parametars.PageSize)
                        .ToListAsync();
        }

        public async Task<int> GetArtilceCount(string filterText)
        {
            return await _context.Articles.AsNoTracking()
                        .Where(a => a.Name.ToLower().Contains(filterText.ToLower().Trim()) || filterText.Trim().Equals(String.Empty))
                        .CountAsync();
        }

        public Task<Article> CreateAsync(Article source)
        {
            return _genericRepository.CreateAsync(source);
        }


        public Task<Article> UpdateAsync(Article source)
        {
            return _genericRepository.UpdateAsync(source);
        }

        public Task<Article> DeleteAsync(int id)
        {

            var deleteingReceiptItems = _context.ReceiptItems.Where(ri => ri.ArticleId == id).ToList();

            foreach (var item in deleteingReceiptItems)
            {
                _context.ReceiptItems.Remove(item);
            }

            _context.SaveChanges();

            return _genericRepository.DeleteAsync(id);
        }

        #endregion

    }
}
