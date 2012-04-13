using System.Collections.Generic;

namespace BLL.Model.Repositories
{
    public interface IRepository<T> where T : class 
    {
        T Insert(T entity);
        T Delete(int id);
        bool Update(T entity);

        T Get(int id);
        IList<T> GetAll();
    }
}
