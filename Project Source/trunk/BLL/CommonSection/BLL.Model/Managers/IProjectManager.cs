using System.Collections.Generic;
using BLL.Model.Entity;

namespace BLL.Model.Managers
{
    public interface IProjectManager
    {
        IList<Project> GetProjects(bool bringInactive = true);
        bool Add(Project project);
        bool Update(Project project);
        int RemoveHeadsFromProject(Project project, IList<Head> heads);
        int AddHeadsToProject(Project project, IList<Head> heads);
        bool IsRecordFound(Project project, Head head);
    }
}

