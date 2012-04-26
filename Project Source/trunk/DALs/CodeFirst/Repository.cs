using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using BLL.Model.Repositories;

namespace CodeFirst
{
    public abstract class GenericRepository<T> :
        IRepository<T>
        where T : class, new()
    {

        private readonly SmartAccountContext _entities = SmartAccountContext.Instance;

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public virtual bool Insert(T entity)
        {
            _entities.Set<T>().Add(entity);
            return true;
        }

        public virtual T Delete(T entity)
        {
           return  _entities.Set<T>().Remove(entity);
        }

        public virtual bool Update(T entity)
        {
            _entities.Entry(entity).State = System.Data.EntityState.Modified;
            return true;
        }

        public virtual int Save()
        {
            return _entities.SaveChanges();
        }

        public bool Discard()
        {
            //TODO: do some context discard method
            throw new NotImplementedException();
        }


        public T Get(int id)
        {
            return _entities.Set<T>().Find(id);
        }
    }
}

