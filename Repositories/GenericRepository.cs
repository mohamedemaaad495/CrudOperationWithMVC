using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;

        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T Entity)
        {
             await _context.Set<T>().AddAsync(Entity);
             _context.SaveChanges();
             return Entity;
        }
        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return (entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> Update(T Entity)
        {
            _context.Set<T>().Update(Entity);
            _context.SaveChanges();
            return Entity;
        }

    }
}
