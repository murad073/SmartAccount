using System.Collections.Generic;
using BLL.Model.Entity;

namespace BLL.Model.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Project Get(string projectName);
        //IList<Head> GetAllHeads(int projectId);
        bool RemoveHeadFromProject(int projectId, int headId);
        bool AddHeadToProject(int projectId, int headId);
        int GetProjectHeadId(string projectName, string headName);
    }
}
