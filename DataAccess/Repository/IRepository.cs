using System.Linq.Expressions;

namespace Project_BookStore.DataAccess.IRepository
{
    public interface IRepository<T> where T : class
    {
        //IEnumerable<T> GetAll(string? includeProperties = null);
        //T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        //void Add(T entity);
        //void Remove(T entity);
        //void RemoveRange(T entity);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
