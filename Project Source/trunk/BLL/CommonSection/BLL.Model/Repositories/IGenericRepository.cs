using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Model.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T Get(int id);
        bool Insert(T entity);
        T Delete(T entity);
        bool Update(T entity);
        int Save();
        bool Discard();
    }
}

