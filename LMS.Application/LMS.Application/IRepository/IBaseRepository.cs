using System.Linq.Expressions;

namespace LMS.Application.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAllQueryable();
        void Remove(T entity);
    }
}
