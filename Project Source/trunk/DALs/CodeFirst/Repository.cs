using System;
using System.Collections.Generic;
using System.Data.Common;
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
        private readonly SmartAccountContext _entities;

        public Repository()
        {
            _entities = new SmartAccountContext();
        }
        //public Repository(string connectionString)
        //{
        //    _entities = new SmartAccountContext(connectionString);
        //}
        public Repository(DbConnection dbConnection)
        {
            _entities = new SmartAccountContext(dbConnection);
        }

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
            int savedCount = _entities.SaveChanges();
            
            //if (savedCount > 0) _entities.Reset();
            return savedCount;
        }

        public void Discard()
        {
            //_entities.Reset();
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

