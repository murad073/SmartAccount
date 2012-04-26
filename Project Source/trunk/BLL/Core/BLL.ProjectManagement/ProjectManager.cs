using System.Collections.Generic;
using BLL.Model;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using System.Linq;
using System;

namespace BLL.ProjectManagement
{
    public class ProjectManager : ManagerBase, IProjectManager
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Head> _headRepository;
        private readonly IRepository<Record> _recordRepository;

        public ProjectManager(IRepository<Project> projectRepository, IRepository<Head> headRepository, IRepository<Record> recordRepository)
        {
            _projectRepository = projectRepository;
            _headRepository = headRepository;
            _recordRepository = recordRepository;
        }

        public IList<Project> GetProjects(bool bringInactive = true)
        {
            if (bringInactive)
                return _projectRepository.GetAll().OrderBy(p => p.Name).ToList();
            return _projectRepository.GetAll().Where(p => p.IsActive).OrderBy(p => p.Name).ToList();
        }

        public bool Add(Project project)
        {
            Project existingProject = _projectRepository.Get(project.Name);

            if (existingProject != null)
            {
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Error, MessageKey = "ProjectAlreadyExists", Parameters = new Dictionary<string, string> { { "ProjectName", project.Name } } });
                return false;
            }

            Project insertedProject = _projectRepository.Insert(project);
            if (insertedProject != null)
            {
                int cashBookId = _headRepository.Get("Cash Book").Id;
                int bankBookId = _headRepository.Get("Bank Book").Id;
                AddHeadsToProject(insertedProject.Id, new int[] { cashBookId, bankBookId });

                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "NewProjectSuccessfullyCreated", Parameters = new Dictionary<string, string> { { "ProjectName", insertedProject.Name } } });

                if (_headRepository.Get(project.Name) == null)
                {
                    Head newHead = new Head
                                       {
                                           Name = insertedProject.Name,
                                           IsActive = true,
                                           Type = HeadType.Capital,
                                           Description =
                                               "This head (related with project '" + project.Name +
                                               "') is only for inter project loan."
                                       };
                    Head insertedHead = _headRepository.Insert(newHead);
                    if (insertedHead != null)
                        InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageDescription = "A head with name '" + project.Name + "' is created for inter project loan." });
                }
                else
                {
                    InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageDescription = "Same head with name '" + project.Name + "' already found." });
                }
                return true;
            }
            return false;
        }

        public bool Update(Project project)
        {
            Project existingProject = _projectRepository.Get(project.Name);

            if (existingProject != null)
            {
                _projectRepository.Update(project);
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "ProjectSuccessfullyUpdated", Parameters = new Dictionary<string, string> { { "ProjectName", existingProject.Name } } });
                return true;
            }
            InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Error, MessageKey = "ProjectUpdatedFailed", Parameters = new Dictionary<string, string> { { "ProjectName", project.Name } } });
            return false;
        }

        public int RemoveHeadsFromProject(int projectId, int[] headIds)
        {
            int count = 0;
            Project project = _projectRepository.Get(projectId);

            if (project == null)
            {
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Warning, MessageDescription = "Invalid project selected." });

                return 0;
            }

            string headNames = "";
            Head head;
            foreach (int headId in headIds)
            {
                head = _headRepository.Get(headId);
                if (head != null)
                {
                    if (_projectRepository.RemoveHeadFromProject(projectId, headId))
                    {
                        headNames += string.IsNullOrWhiteSpace(headNames) ? head.Name : ", " + head.Name;
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                InvokeManagerEvent(new BLLEventArgs
                {
                    EventType = EventType.Success,
                    MessageDescription = count + " head(s) removed from project '" + project.Name + "': " +
                        headNames + "."
                });

            }
            else
            {
                InvokeManagerEvent(new BLLEventArgs
                {
                    EventType = EventType.Information,
                    MessageDescription = "No head(s) removed from project '" + project.Name + "'."
                });
            }
            return count;
        }

        public int AddHeadsToProject(int projectId, int[] headIds)
        {
            int count = 0;
            Project project = _projectRepository.Get(projectId);

            if (project == null)
            {
                InvokeManagerEvent(new BLLEventArgs
                {
                    EventType = EventType.Warning,
                    MessageDescription = "Invalid project selected."
                });
                return 0;
            }

            string headNames = "";
            Head head;
            foreach (int headId in headIds)
            {
                head = _headRepository.Get(headId);
                if (head != null)
                {
                    if (_projectRepository.AddHeadToProject(projectId, headId))
                    {
                        headNames += string.IsNullOrWhiteSpace(headNames) ? head.Name : ", " + head.Name;
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                InvokeManagerEvent(new BLLEventArgs
                {
                    EventType = EventType.Success,
                    MessageDescription = count + " head(s) added to project '" + project.Name + "': " + headNames + "."
                });
            }
            else
            {
                InvokeManagerEvent(new BLLEventArgs
                {
                    EventType = EventType.Information,
                    MessageDescription = "No head(s) added to project '" + project.Name + "'."
                });
            }
            return count;
        }

        public bool IsRecordFound(int projectId, int headId)
        {
            return _recordRepository.IsRecordFound(projectId, headId);
        }
    }
}


