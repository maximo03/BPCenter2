using BPCenter2.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BPCenter2.Services
{
    public interface IServicesDB<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(string id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(string id);

        Task ProcedureUpdatePassword(string id, string password);

        Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes);
    }

    public class ServicesDB<T> : IServicesDB<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public ServicesDB(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var entity = await _dbSet.FindAsync(id) ??
                throw new ArgumentNullException(nameof(id), "El objeto no puede ser nulo.");
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task ProcedureUpdatePassword(string id, string password)
        {

            await _context.Database.ExecuteSqlRawAsync($"SP_UpdatePassword '{id}','{password}'");

        }

        public async Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
    }
}
