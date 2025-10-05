using LMS.Application.IRepository;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LMS.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDBContext db)
        {
            this._dbSet = db.Set<T>();
            //db.Categories = dbSet
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;
            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public IQueryable<T> GetAllQueryable()
        {
            IQueryable<T> query = _dbSet;
            return query.AsQueryable();
        }
        public void Remove(T entity)
        {
            _dbSet.Remove(entity); 
        }

    }
}
