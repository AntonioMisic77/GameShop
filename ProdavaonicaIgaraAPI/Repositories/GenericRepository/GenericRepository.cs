using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Models;

namespace ProdavaonicaIgaraAPI.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly PIGDbContext _context;

        public GenericRepository(PIGDbContext context)
        {
            _context = context;
        }
        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> CreateAsync(T source)
        {
            await  _context.Set<T>().AddAsync(source);
            await _context.SaveChangesAsync();

            return source;
        }

        public async Task<T> UpdateAsync(T source)
        {
            _context.Set<T>().Update(source);
            await _context.SaveChangesAsync();

            return source;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null) return entity;

            _context.Set<T>().Remove(entity);

            await _context.SaveChangesAsync();

            return entity;
        }   

    }
}
