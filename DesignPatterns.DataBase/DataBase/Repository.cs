using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.DataBase.DataBase
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context) => _context = context;

        public void Add(T entity) => _context.Set<T>().Add(entity);

        public void AddRange(IEnumerable<T> entities) => _context.Set<T>().AddRange(entities);

        public Task<bool> AnyAsync() => _context.Set<T>().AsNoTracking().AnyAsync();

        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression) => _context.Set<T>().AsNoTracking().AnyAsync(expression);

        public Task<long> CountAsync() => _context.Set<T>().AsNoTracking().LongCountAsync();

        public Task<long> CountAsync(Expression<Func<T, bool>> expression) => _context.Set<T>().AsNoTracking().LongCountAsync(expression);

        public void Delete(object key)
        {
            var entity = _context.Set<T>().Find(key);

            if (entity is null) return;

            _context.Set<T>().Remove(entity);
        }

        public Task<T> GetAsync(object key) => _context.Set<T>().FindAsync(key).AsTask();

        public async Task<IEnumerable<T>> ListAsync() => await _context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression) => await _context.Set<T>().Where(expression).ToListAsync();

        public void Update(T entity)
        {
            var primaryKey = _context.PrimaryKey(entity);

            var contextEntity = _context.Set<T>().Find(primaryKey);

            if (contextEntity is null) return;

            _context.Entry(contextEntity).State = EntityState.Detached;

            _context.Update(entity);
        }

        public void UpdatePartial(object entity)
        {
            var primaryKey = _context.PrimaryKey(entity);

            var contextEntity = _context.Set<T>().Find(primaryKey);

            if (contextEntity is null) return;

            var entry = _context.Entry(contextEntity);

            entry.CurrentValues.SetValues(entity);

            foreach (var navigation in entry.Metadata.GetNavigations())
            {
                if (navigation.IsOnDependent || navigation.IsCollection || !navigation.ForeignKey.IsOwnership) continue;

                var property = entity.GetType().GetProperty(navigation.Name);

                if (property is null) continue;

                var value = property.GetValue(entity);

                entry.Reference(navigation.Name).TargetEntry?.CurrentValues.SetValues(value);
            }
        }

        public void UpdateRange(IEnumerable<T> entities) => _context.Set<T>().UpdateRange(entities);
    }
}
