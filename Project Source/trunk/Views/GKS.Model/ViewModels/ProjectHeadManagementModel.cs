using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BLL.Factories;
using BLL.Messaging;
using BLL.Model.Entity;
using BLL.Model.Managers;
using GKS.Factory;

namespace GKS.Model.ViewModels
{
    public class ProjectHeadManagementModel : INotifyPropertyChanged
    {
        private readonly IProjectManager _projectManager;
        private readonly IHeadManager _headManager;

        private IList<Head> _allHeads;

        public ProjectHeadManagementModel()
        {
            _projectManager = BLLCoreFactory.GetProjectManager();
            _headManager = BLLCoreFactory.GetHeadManager();

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
                    HeadsForProject = _headManager.GetHeads(value, false);//.Select(p => new KeyValuePair<int, string>(p.ID, p.Name)).ToArray();
                    //int[] ids = _headsForProject.Select(c => c.Key).ToArray();
                    RemainingHeads = _allHeads.Except(HeadsForProject).ToList();// .Where(c => !ids.Contains(c.ID) && c.Name != ProjectSelected.Name).Select(p => new KeyValuePair<int, string>(p.ID, p.Name)).ToArray();
                }
                SelectedRemainingHead = SelectedHeadForProject = null;
            }
        }

        // Remaining Heads list box - right sided
        private IList<Head> _remainingHeads;
        public IList<Head> RemainingHeads
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

        private Head _selectedRemainingHead;
        public Head SelectedRemainingHead
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
        private IList<Head> _headsForProject;
        public IList<Head> HeadsForProject
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

        private Head _selectedHeadForProject;
        public Head SelectedHeadForProject
        {
            get
            {
                return _selectedHeadForProject;
            }
            set
            {
                _selectedHeadForProject = value;
                //if (ProjectSelected != null)
                //    RemoveHeadEnable = !_projectManager.IsRecordFound(ProjectSelected.Id, value.Key);
                //if (PropertyChanged != null)
                //    PropertyChanged(this, new PropertyChangedEventArgs("SelectedHeadForProject"));
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
            AllProjectItems = _projectManager.GetProjects(false);
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
            Head head = _projectHeadModel.SelectedRemainingHead;

            //if (head.Key > 0)
            //{
            List<Head> existingHeads = _projectHeadModel.HeadsForProject.ToList();
            existingHeads.Add(head);
            _projectHeadModel.HeadsForProject = existingHeads.ToArray();

            //Head removableHead = _projectHeadModel.RemainingHeads.Where(rc => rc.Key == head.Key).SingleOrDefault();
            List<Head> existingRemovableHeads = _projectHeadModel.RemainingHeads.ToList();
            //existingRemovableHeads.Remove(removableHead);
            _projectHeadModel.RemainingHeads = existingRemovableHeads.ToArray();

            _projectHeadModel.SelectedHeadForProject = head;
            _projectHeadModel.SelectedRemainingHead = null;
            //}
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
            Head head = _projectHeadModel.SelectedHeadForProject;

            //if (head.Key > 0)
            //{
                List<Head> existingHeads = _projectHeadModel.RemainingHeads.ToList();
                existingHeads.Add(head);
                _projectHeadModel.RemainingHeads = existingHeads.ToArray();

                //KeyValuePair<int, string> removableHead = _projectHeadModel.HeadsForProject.Where(cp => cp.Key == head.Key).SingleOrDefault();
                //List<KeyValuePair<int, string>> existingRemovableHeads = _projectHeadModel.HeadsForProject.ToList();
                //existingRemovableHeads.Remove(removableHead);
                //_projectHeadModel.HeadsForProject = existingRemovableHeads.ToArray();

                //_projectHeadModel.SelectedRemainingHead = head;
                //_projectHeadModel.SelectedHeadForProject = new KeyValuePair<int, string>(0, null);
            //}
        }
    }

    public class SaveProjectHeadRelation : ICommand
    {
        ProjectHeadManagementModel _projectHeadModel;
        private IHeadManager _headManager;
        private IProjectManager _projectManager;
        public SaveProjectHeadRelation(ProjectHeadManagementModel model)
        {
            _projectHeadModel = model;
            _headManager = BLLCoreFactory.GetHeadManager();
            _projectManager = BLLCoreFactory.GetProjectManager();
        }

        public bool CanExecute(object parameter)
        {
            return _projectHeadModel.ProjectSelected != null;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            MessageService.Instance.Reset();
            string message = "";

            if (_projectHeadModel.RemainingHeads != null && _projectHeadModel.RemainingHeads.Count() > 0)
            {
                IList<Head> removeHeads = _projectHeadModel.RemainingHeads;//.Select(rc => rc.Key).ToArray();
                int removedItems = _projectManager.RemoveHeadsFromProject(_projectHeadModel.ProjectSelected,
                                                                          _projectHeadModel.RemainingHeads);
                Message removedMessage = MessageService.Instance.GetLatestMessage();
                if (removedItems > 0) message += removedMessage.MessageText + Environment.NewLine;
            }
            if (_projectHeadModel.HeadsForProject != null && _projectHeadModel.HeadsForProject.Count() > 0)
            {
                IList<Head> addedHeads = _projectHeadModel.HeadsForProject;//.Select(cp => cp.Key).ToArray();
                int addedItems = _projectManager.AddHeadsToProject(_projectHeadModel.ProjectSelected, addedHeads);
                Message addedMessage = MessageService.Instance.GetLatestMessage();
                if (addedItems > 0) message += addedMessage.MessageText;
            }

            _projectHeadModel.NotificationMessage = message;
        }
    }
}


