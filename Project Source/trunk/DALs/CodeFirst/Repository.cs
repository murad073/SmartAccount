using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using BLL.Model.Repositories;

namespace CodeFirst
{
    public class Repository<T> :
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

        public virtual void Insert(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
             _entities.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {
            _entities.Entry(entity).State = System.Data.EntityState.Modified;
        }

        public virtual int Save()
        {
            return _entities.SaveChanges();
        }

        public void Discard()
        {
            _entities.Reset();
        }

        public T Get(int id)
        {
            return _entities.Set<T>().Find(id);
        }

        public T GetSingle(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Get(predicate).SingleOrDefault();
        }
    }
}

