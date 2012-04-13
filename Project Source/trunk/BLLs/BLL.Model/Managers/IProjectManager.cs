using System.Collections.Generic;
using BLL.Model.Schema;

namespace BLL.Model.Managers
{
    public interface IProjectManager
    {
        IList<Project> GetProjects(bool bringInactive = true);
        bool Add(Project project);
        bool Update(Project project);
        int RemoveHeadsFromProject(int projectId, int[] headIds);
        int AddHeadsToProject(int projectId, int[] headIds);
        bool IsRecordFound(int projectId, int headId);
    }
}