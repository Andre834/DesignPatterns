using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.DataBase.DataBase
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        Task<bool> AnyAsync();

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task<long> CountAsync();

        Task<long> CountAsync(Expression<Func<T, bool>> expression);

        void Delete(object key);

        Task<T> GetAsync(object key);

        Task<IEnumerable<T>> ListAsync();

        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression);

        void Update(T entity);

        void UpdatePartial(object entity);

        void UpdateRange(IEnumerable<T> entities);
    }
}
