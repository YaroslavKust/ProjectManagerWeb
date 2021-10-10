using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProjectManager.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}