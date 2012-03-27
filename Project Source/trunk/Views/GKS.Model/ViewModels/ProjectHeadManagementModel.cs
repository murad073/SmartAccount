using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BLL.Messaging;
using BLL.Model.Schema;
using BLL.ProjectManagement;
using GKS.Factory;

namespace GKS.Model.ViewModels
{
    public class ProjectHeadManagementModel : INotifyPropertyChanged
    {
        private readonly ProjectManager _projectManager;
        private readonly HeadManager _headManager;

        private IList<Head> _allHeads;

        public ProjectHeadManagementModel()
        {
            _projectManager = new ProjectManager(GKSFactory.GetProjectRepository(), GKSFactory.GetHeadRepository(), GKSFactory.GetRecordRepository());
            _headManager = new HeadManager(GKSFactory.GetHeadRepository());

            _allHeads = _headManager.GetHeads(false, false);

            AllProjectItems = _projectManager.GetProjects(false);

            AddHeadButtonClicked = new AddHeadInProjet(this);
            RemoveHeadButtonClicked = new RemoveHeadFromProjet(this);
            SaveProjectsForHead = new SaveProjectHeadRelation(this);
        }

        // Project list box - left sided
        private IList<Project> _allProjectItems;
        public IList<Project> AllProjectItems
        {
            get
            {
                return _allProjectItems;
            }
            set
            {
                _allProjectItems = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("AllProjectItems"));
            }
        }

        private Project _projectSelected;
        public Project ProjectSelected
        {
            get
            {
                return _projectSelected;
            }
            set
            {
                _projectSelected = value;
                if (value == null)
                {
                    HeadsForProject = null;
                    RemainingHeads = null;
                }
                else
                {
                    HeadsForProject = _headManager.GetHeads(value.Id, false).Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToArray();
                    int[] ids = _headsForProject.Select(c => c.Key).ToArray();
                    RemainingHeads = _allHeads.Where(c => !ids.Contains(c.Id) && c.Name != ProjectSelected.Name).Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToArray();
                }
                SelectedRemainingHead = SelectedHeadForProject = new KeyValuePair<int, string>(0, null);
            }
        }

        // Remaining Heads list box - right sided
        private KeyValuePair<int, string>[] _remainingHeads;
        public KeyValuePair<int, string>[] RemainingHeads
        {
            get
            {
                return _remainingHeads;
            }
            set
            {
                _remainingHeads = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("RemainingHeads"));
            }

        }

        private KeyValuePair<int, string> _selectedRemainingHead;
        public KeyValuePair<int, string> SelectedRemainingHead
        {
            get
            {
                return _selectedRemainingHead;
            }

            set
            {
                _selectedRemainingHead = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedRemainingHead"));
                }
            }
        }

        // Project-Head list box - middle placed
        private KeyValuePair<int, string>[] _headsForProject;
        public KeyValuePair<int, string>[] HeadsForProject
        {
            get
            {
                return _headsForProject;
            }
            set
            {
                _headsForProject = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("HeadsForProject"));
            }
        }

        private KeyValuePair<int, string> _selectedHeadForProject;
        public KeyValuePair<int, string> SelectedHeadForProject
        {
            get
            {
                return _selectedHeadForProject;
            }
            set
            {
                _selectedHeadForProject = value;
                if (ProjectSelected != null)
                    RemoveHeadEnable = !_projectManager.IsRecordFound(ProjectSelected.Id, value.Key);
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedHeadForProject"));
            }
        }

        private string _notificationMessage;
        public string NotificationMessage
        {
            get { return _notificationMessage; }
            set
            {
                _notificationMessage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("NotificationMessage"));
            }
        }

        private bool _removeHeadEnable;
        public bool RemoveHeadEnable
        {
            get
            {
                return _removeHeadEnable;
            }
            set
            {
                _removeHeadEnable = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("RemoveHeadEnable"));
                }
            }
        }

        public ICommand AddHeadButtonClicked { get; set; }
        public ICommand RemoveHeadButtonClicked { get; set; }
        public ICommand SaveProjectsForHead { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Reset()
        {
            AllProjectItems = new List<Project>(_projectManager.GetProjects(false));
            _allHeads = _headManager.GetHeads(false, false);
            ProjectSelected = null;
            NotificationMessage = "";
        }
    }

    public class AddHeadInProjet : ICommand
    {
        ProjectHeadManagementModel _projectHeadModel;
        public AddHeadInProjet(ProjectHeadManagementModel projectHeadModel)
        {
            _projectHeadModel = projectHeadModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            KeyValuePair<int, string> head = _projectHeadModel.SelectedRemainingHead;

            if (head.Key > 0)
            {

                List<KeyValuePair<int, string>> existingHeads = _projectHeadModel.HeadsForProject.ToList();
                existingHeads.Add(head);
                _projectHeadModel.HeadsForProject = existingHeads.ToArray();

                KeyValuePair<int, string> removableHead = _projectHeadModel.RemainingHeads.Where(rc => rc.Key == head.Key).SingleOrDefault();
                List<KeyValuePair<int, string>> existingRemovableHeads = _projectHeadModel.RemainingHeads.ToList();
                existingRemovableHeads.Remove(removableHead);
                _projectHeadModel.RemainingHeads = existingRemovableHeads.ToArray();

                _projectHeadModel.SelectedHeadForProject = head;
                _projectHeadModel.SelectedRemainingHead = new KeyValuePair<int, string>(0, null);
            }
        }
    }

    public class RemoveHeadFromProjet : ICommand
    {
        ProjectHeadManagementModel _projectHeadModel;
        public RemoveHeadFromProjet(ProjectHeadManagementModel projectHeadModel)
        {
            _projectHeadModel = projectHeadModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            KeyValuePair<int, string> head = _projectHeadModel.SelectedHeadForProject;

            if (head.Key > 0)
            {
                List<KeyValuePair<int, string>> existingHeads = _projectHeadModel.RemainingHeads.ToList();
                existingHeads.Add(head);
                _projectHeadModel.RemainingHeads = existingHeads.ToArray();

                KeyValuePair<int, string> removableHead = _projectHeadModel.HeadsForProject.Where(cp => cp.Key == head.Key).SingleOrDefault();
                List<KeyValuePair<int, string>> existingRemovableHeads = _projectHeadModel.HeadsForProject.ToList();
                existingRemovableHeads.Remove(removableHead);
                _projectHeadModel.HeadsForProject = existingRemovableHeads.ToArray();

                _projectHeadModel.SelectedRemainingHead = head;
                _projectHeadModel.SelectedHeadForProject = new KeyValuePair<int, string>(0, null);
            }
        }
    }

    public class SaveProjectHeadRelation : ICommand
    {
        ProjectHeadManagementModel _projectHeadModel;
        private HeadManager _headManager;
        private ProjectManager _projectManager;
        public SaveProjectHeadRelation(ProjectHeadManagementModel model)
        {
            _projectHeadModel = model;
            _headManager = new HeadManager(GKSFactory.GetHeadRepository());
            _projectManager = new ProjectManager(GKSFactory.GetProjectRepository(), GKSFactory.GetHeadRepository(), GKSFactory.GetRecordRepository());
        }

        public bool CanExecute(object parameter)
        {
            return _projectHeadModel.ProjectSelected != null;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            string message = "";
            if (_projectHeadModel.RemainingHeads != null && _projectHeadModel.RemainingHeads.Count() > 0)
            {
                int[] removeHeadIds = _projectHeadModel.RemainingHeads.Select(rc => rc.Key).ToArray();
                int removedItems = _projectManager.RemoveHeadsFromProject(_projectHeadModel.ProjectSelected.Id,
                                                                          removeHeadIds);
                Message removedMessage = _projectManager.GetLatestMessage();
                if (removedItems > 0) message += removedMessage.MessageText + Environment.NewLine;
            }
            if (_projectHeadModel.HeadsForProject != null && _projectHeadModel.HeadsForProject.Count() > 0)
            {
                int[] addedHeadIds = _projectHeadModel.HeadsForProject.Select(cp => cp.Key).ToArray();
                int addedItems = _projectManager.AddHeadsToProject(_projectHeadModel.ProjectSelected.Id, addedHeadIds);
                Message addedMessage = _projectManager.GetLatestMessage();
                if (addedItems > 0) message += addedMessage.MessageText;
            }

            _projectHeadModel.NotificationMessage = message;
        }
    }
}


