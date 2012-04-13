using System.Collections.Generic;
using BLL.Model.Schema;

namespace BLL.Model.Repositories
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        IList<Budget> GetAll(int projectId);
        Budget GetByProjectHeadId(int projectHeadId);
    }
}
