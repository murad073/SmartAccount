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
        private readonly IRepository<ProjectHead> _projectHeadRepository;
        private readonly IRepository<Head> _headRepository;
        private readonly IRepository<Record> _recordRepository;

        public ProjectManager(IRepository<Project> projectRepository, IRepository<Head> headRepository, IRepository<ProjectHead> projectHeadRepository, IRepository<Record> recordRepository)
        {
            _projectRepository = projectRepository;
            _headRepository = headRepository;
            _projectHeadRepository = projectHeadRepository;
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
            Project existingProject = _projectRepository.GetSingle(p => p.Name == project.Name);

            if (existingProject != null)
            {
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Error, MessageKey = "ProjectAlreadyExists", Parameters = new Dictionary<string, string> { { "ProjectName", project.Name } } });
                return false;
            }

            _projectRepository.Insert(project);
            //if (insertedProject != null)
            //{
            Head cashBook = _headRepository.GetSingle(h => h.Name == "Cash Book");
            Head bankBook = _headRepository.GetSingle(h => h.Name == "Bank Book");

            _projectHeadRepository.Insert(new ProjectHead() { Project = project, Head = cashBook, IsActive = true });
            _projectHeadRepository.Insert(new ProjectHead() { Project = project, Head = bankBook, IsActive = true });

            //AddHeadsToProject(insertedProject.Id, new int[] { cashBookId, bankBookId });
            if (_projectRepository.Save() > 0)
            {
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "NewProjectSuccessfullyCreated", Parameters = new Dictionary<string, string> { { "ProjectName", project.Name } } });

                if (_headRepository.GetSingle(h => h.Name == project.Name) == null)
                {
                    Head newHead = new Head
                                       {
                                           Name = project.Name,
                                           IsActive = true,
                                           Type = "Capital",
                                           Description =
                                               "This head (related with project '" + project.Name +
                                               "') is only for inter project loan."
                                       };
                    _headRepository.Insert(newHead);
                    if (_headRepository.Save() > 0)
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
            //Project existingProject = _projectRepository.Get(project.Name);

            //if (existingProject != null)
            //{
            _projectRepository.Update(project);
            if (_projectRepository.Save() > 0)
            {
                InvokeManagerEvent(new BLLEventArgs
                                       {
                                           EventType = EventType.Success,
                                           MessageKey = "ProjectSuccessfullyUpdated",
                                           Parameters =
                                               new Dictionary<string, string> { { "ProjectName", project.Name } }
                                       });
                return true;
            }
            //}
            InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Error, MessageKey = "ProjectUpdatedFailed", Parameters = new Dictionary<string, string> { { "ProjectName", project.Name } } });
            return false;
        }

        public int RemoveHeadsFromProject(Project project, IList<Head> heads)
        {
            int count = 0;
            //Project project = _projectRepository.Get(projectId);

            //if (project == null)
            //{
            //    InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Warning, MessageDescription = "Invalid project selected." });
            //    return 0;
            //}
            //_projectHeadRepository.Get(ph=>ph.)

            foreach (Head deletableHead in heads)
            {
                ProjectHead deletableProjectHead = _projectHeadRepository.GetSingle(ph => ph.Project.ID == project.ID && ph.Head.ID == deletableHead.ID);
                project.ProjectHeads.Remove(deletableProjectHead);
            }

            return _projectHeadRepository.Save();
            //string headNames = "";
            //Head head;
            //foreach (int headId in headIds)
            //{
            //    head = _headRepository.Get(headId);
            //    if (head != null)
            //    {
            //        if (_projectRepository.RemoveHeadFromProject(projectId, headId))
            //        {
            //            headNames += string.IsNullOrWhiteSpace(headNames) ? head.Name : ", " + head.Name;
            //            count++;
            //        }
            //    }
            //}

            //if (count > 0)
            //{
            //    InvokeManagerEvent(new BLLEventArgs
            //    {
            //        EventType = EventType.Success,
            //        MessageDescription = count + " head(s) removed from project '" + project.Name + "': " +
            //            headNames + "."
            //    });

            //}
            //else
            //{
            //    InvokeManagerEvent(new BLLEventArgs
            //    {
            //        EventType = EventType.Information,
            //        MessageDescription = "No head(s) removed from project '" + project.Name + "'."
            //    });
            //}
            //return count;
        }

        public int AddHeadsToProject(Project project, IList<Head> heads)
        {
            int count = 0;
            //Project project = _projectRepository.Get(projectId);

            foreach (Head addableHead in heads)
            {
                ProjectHead projectHead =
                    _projectHeadRepository.GetSingle(ph => ph.Project.ID == project.ID && ph.Head.ID == addableHead.ID);
                if (projectHead == null)
                {
                    ProjectHead newProjectHead = new ProjectHead { Project = project, Head = addableHead, IsActive = true };
                    _projectHeadRepository.Insert(newProjectHead);
                }
                //else _projectHeadRepository.Update(projectHead);
            }

            return _projectHeadRepository.Save();
            //if (project == null)
            //{
            //    InvokeManagerEvent(new BLLEventArgs
            //    {
            //        EventType = EventType.Warning,
            //        MessageDescription = "Invalid project selected."
            //    });
            //    return 0;
            //}

            //string headNames = "";
            //Head head;
            //foreach (int headId in headIds)
            //{
            //    head = _headRepository.Get(headId);
            //    if (head != null)
            //    {
            //        if (_projectRepository.AddHeadToProject(projectId, headId))
            //        {
            //            headNames += string.IsNullOrWhiteSpace(headNames) ? head.Name : ", " + head.Name;
            //            count++;
            //        }
            //    }
            //}

            //if (count > 0)
            //{
            //    InvokeManagerEvent(new BLLEventArgs
            //    {
            //        EventType = EventType.Success,
            //        MessageDescription = count + " head(s) added to project '" + project.Name + "': " + headNames + "."
            //    });
            //}
            //else
            //{
            //    InvokeManagerEvent(new BLLEventArgs
            //    {
            //        EventType = EventType.Information,
            //        MessageDescription = "No head(s) added to project '" + project.Name + "'."
            //    });
            //}
            //return count;
        }

        //public bool IsRecordFound(int projectId, int headId)
        //{
        //    return _recordRepository.IsRecordFound(projectId, headId);
        //}
    }
}


