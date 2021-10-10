using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _dataSet;
        private DbContext _context;

        protected DbSet<T> DataSet => _dataSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dataSet = context.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression = null)
        {
            return expression == null ? _dataSet.AsNoTracking() : _dataSet.Where(expression).AsNoTracking();
        }

        public void Add(T entity)
        {
            _dataSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dataSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity) 
        {
            _dataSet.Attach(entity);
            _dataSet.Remove(entity);
        }
    }
}