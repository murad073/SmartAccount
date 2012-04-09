using System.Collections.Generic;
using BLL.Messaging;
using BLL.Model.Repositories;
using BLL.Model.Schema;
using System.Linq;
using System;

namespace BLL.ProjectManagement
{
    public class ProjectManager
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IHeadRepository _headRepository;
        private readonly IRecordRepository _recordRepository;
        private  Message _message;

        public ProjectManager(IProjectRepository projectRepository, IHeadRepository headRepository, IRecordRepository recordRepository)
        {
            _projectRepository = projectRepository;
            _headRepository = headRepository;
            _recordRepository = recordRepository;
            _message = new Message();
        }

        public IList<Project> GetProjects(bool bringInactive = true)
        {
            if (bringInactive)
                return _projectRepository.GetAll().OrderBy(p => p.Name).ToList();
            return _projectRepository.GetAll().Where(p => p.IsActive).OrderBy(p => p.Name).ToList();
        }

        public Project GetDefaultProject()
        {
            return new Project();
            //TODO : get default project from Parameter field of database
        }

        public bool Add(Project project)
        {
            Project existingProject = _projectRepository.Get(project.Name);

            if (existingProject != null)
            {
                _message = MessageService.Instance.Get("ProjectAlreadyExists", MessageType.Error);
                _message.MessageText = string.Format(_message.MessageText, project.Name);
                return false;
            }

            Project insertedProject = _projectRepository.Insert(project);
            if (insertedProject != null)
            {
                int cashBookId = _headRepository.Get("Cash Book").Id;
                int bankBookId = _headRepository.Get("Bank Book").Id;
                //int advanceId = _headRepository.Get("Advance").Id;
                AddHeadsToProject(insertedProject.Id, new int[] { cashBookId, bankBookId });

                _message = MessageService.Instance.Get("NewProjectSuccessfullyCreated", MessageType.Success);
                _message.MessageText = string.Format(_message.MessageText, insertedProject.Name);

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
                        _message.MessageText += Environment.NewLine + "A head with name '" + project.Name + "' is created for inter project loan.";
                }
                else
                {
                    _message.MessageText += Environment.NewLine + "Same head with name '" + project.Name + "' already found.";
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
                _message = MessageService.Instance.Get("ProjectSuccessfullyUpdated", MessageType.Success);
                _message.MessageText = string.Format(_message.MessageText, existingProject.Name);
                return true;
            }
            _message = MessageService.Instance.Get("ProjectUpdatedFailed", MessageType.Error);
            _message.MessageText = string.Format(_message.MessageText, project.Name);
            return false;
        }

        public int RemoveHeadsFromProject(int projectId, int[] headIds)
        {
            int count = 0;
            Project project = _projectRepository.Get(projectId);

            if (project == null)
            {
                _message.MessageText = "Invalid project selected.";
                _message.MessageType = MessageType.Warning;
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
                _message.MessageText = count + " head(s) removed from project '" + project.Name + "': " +
                                       headNames + ".";
                _message.MessageType = MessageType.Success;
            }
            else
            {
                _message.MessageText = "No head(s) removed from project '" + project.Name + "'.";
                _message.MessageType = MessageType.Information;
            }
            return count;
        }

        public int AddHeadsToProject(int projectId, int[] headIds)
        {
            int count = 0;
            Project project = _projectRepository.Get(projectId);

            if (project == null)
            {
                _message.MessageText = "Invalid project selected.";
                _message.MessageType = MessageType.Warning;
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
                _message.MessageText = count + " head(s) added to project '" + project.Name + "': " +
                                       headNames + ".";
                _message.MessageType = MessageType.Success;
            }
            else
            {
                _message.MessageText = "No head(s) added to project '" + project.Name + "'.";
                _message.MessageType = MessageType.Information;
            }
            return count;
        }

        public bool IsRecordFound(int projectId, int headId)
        {
            return _recordRepository.IsRecordFound(projectId, headId);
        }

        public Message GetLatestMessage()
        {
            return _message;
        }
    }
}


