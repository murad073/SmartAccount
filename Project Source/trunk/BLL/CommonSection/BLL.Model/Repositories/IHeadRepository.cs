using System.Collections.Generic;
using BLL.Model.Entity;

namespace BLL.Model.Repositories
{
    public interface IHeadRepository : IRepository<Head>
    {
        Head Get(string headName);
        IList<Head> GetAll(int projectId);
    }
}
