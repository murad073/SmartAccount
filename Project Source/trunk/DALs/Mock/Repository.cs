using System;
using System.Linq;
using BLL.Model.Repositories;

namespace Mock
{
    public class MockRepository<T> :
        IRepository<T>
        where T : class, new()
    {
        private MockEntities  _entities = new MockEntities();

        public IQueryable<T> GetAll()
        {
            return _entities.GetTable<T>() as IQueryable<T>;
        }

        public IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T GetSingle(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public void Discard()
        {
            throw new NotImplementedException();
        }
    }
}

