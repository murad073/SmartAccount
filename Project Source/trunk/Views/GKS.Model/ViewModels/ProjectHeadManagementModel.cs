using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ProjectHeadManagementModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IHeadManager _headManager;

        private IList<Head> _allHeads;

        public ProjectHeadManagementModel()
        {
            _projectManager = BLLCoreFactory.GetProjectManager();
            _headManager = BLLCoreFactory.GetHeadManager();
            _allHeads = _headManager.GetHeads(false, false);

            AllProjectItems = new ObservableCollection<Project>(_projectManager.GetProjects(false));
        }

        // Project list box - left sided
        private ObservableCollection<Project> _allProjectItems;
        public ObservableCollection<Project> AllProjectItems
        {
            get
            {
                return _allProjectItems;
            }
            set
            {
                _allProjectItems = value;
                NotifyPropertyChanged("AllProjectItems");
            }
        }

        private Project _selectedProject;
        public Project SelectedProject
        {
            get
            {
                return _selectedProject;
            }
            set
            {
                _selectedProject = value;
                if (value == null)
                {
                    HeadsForProject = null;
                    RemainingHeads = null;
                }
                else
                {
                    HeadsForProject = new ObservableCollection<Head>(_headManager.GetHeads(value, false));
                    RemainingHeads = new ObservableCollection<Head>(_allHeads.Except(HeadsForProject));
                }
                SelectedRemainingHead = SelectedHeadForProject = null;
            }
        }

        // Remaining Heads list box - right sided
        private ObservableCollection<Head> _remainingHeads;
        public ObservableCollection<Head> RemainingHeads
        {
            get
            {
                return _remainingHeads;
            }
            set
            {
                _remainingHeads = value;
                NotifyPropertyChanged("RemainingHeads");
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
                NotifyPropertyChanged("SelectedRemainingHead");
            }
        }

        // Project-Head list box - middle placed
        private ObservableCollection<Head> _headsForProject;
        public ObservableCollection<Head> HeadsForProject
        {
            get
            {
                return _headsForProject;
            }
            set
            {
                _headsForProject = value;
                NotifyPropertyChanged("HeadsForProject");
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
                RemoveHeadEnable = !_projectManager.IsRecordFound(SelectedProject, value);
                NotifyPropertyChanged("SelectedHeadForProject");
            }
        }

        private string _notificationMessage;
        public string NotificationMessage
        {
            get { return _notificationMessage; }
            set
            {
                _notificationMessage = value;
                NotifyPropertyChanged("NotificationMessage");
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
                NotifyPropertyChanged("RemoveHeadEnable");
            }
        }

        #region relay commands

        private RelayCommand _resetButtonClicked;
        public ICommand ResetButtonClicked
        {
            get
            {
                return _resetButtonClicked ??
                       (_resetButtonClicked = new RelayCommand(p1 => this.Reset()));
            }
        }


        private RelayCommand _removeHeadButtonClicked;
        public ICommand RemoveHeadButtonClicked
        {
            get
            {
                return _removeHeadButtonClicked ??
                       (_removeHeadButtonClicked = new RelayCommand(p1 => this.RemoveHeadFromProjetListToRemainingList(), p2 => SelectedHeadForProject != null));
            }
        }

        private RelayCommand _saveProjectsForHead;
        public ICommand SaveProjectsForHead
        {
            get
            {
                return _saveProjectsForHead ??
                       (_saveProjectsForHead = new RelayCommand(p1 => this.SaveButtonClicked()));
            }
        }

        private RelayCommand _addHeadButtonClicked;
        public ICommand AddHeadButtonClicked
        {
            get
            {
                return _addHeadButtonClicked ??
                       (_addHeadButtonClicked = new RelayCommand(p1 => this.AddHeadInProjetListFromRemainingList(), p2 => SelectedRemainingHead != null));
            }
        }

        private void AddHeadInProjetListFromRemainingList()
        {
            HeadsForProject.Add(SelectedRemainingHead);
            RemainingHeads.Remove(SelectedRemainingHead);
            SelectedHeadForProject = SelectedRemainingHead;
            SelectedRemainingHead = null;
        }

        private void RemoveHeadFromProjetListToRemainingList()
        {
            RemainingHeads.Add(SelectedHeadForProject);
            HeadsForProject.Remove(SelectedHeadForProject);
            SelectedRemainingHead = SelectedHeadForProject;
            SelectedHeadForProject = null;
        }

        private void SaveButtonClicked()
        {
            MessageService.Instance.Reset();
            string message = "";

            if (RemainingHeads != null && RemainingHeads.Count() > 0)
            {
                //IList<Head> removeHeads = _projectHeadModel.RemainingHeads;//.Select(rc => rc.Key).ToArray();
                int removedItems = _projectManager.RemoveHeadsFromProject(SelectedProject, RemainingHeads);
                Message removedMessage = MessageService.Instance.GetLatestMessage();
                if (removedItems > 0) message += removedMessage.MessageText + Environment.NewLine;
            }
            if (HeadsForProject != null && HeadsForProject.Count() > 0)
            {
                //IList<Head> addedHeads = _projectHeadModel.HeadsForProject;//.Select(cp => cp.Key).ToArray();
                int addedItems = _projectManager.AddHeadsToProject(SelectedProject, HeadsForProject);
                Message addedMessage = MessageService.Instance.GetLatestMessage();
                if (addedItems > 0) message += addedMessage.MessageText;
            }
            NotificationMessage = message;
        }
        
        private void Reset()
        {
            AllProjectItems = new ObservableCollection<Project>(_projectManager.GetProjects(false));
            _allHeads = _headManager.GetHeads(false, false);
            SelectedProject = null;
            NotificationMessage = "";
        }

        #endregion
    }

    //public class AddHeadInProjet : ICommand
    //{
    //    ProjectHeadManagementModel _projectHeadModel;
    //    public AddHeadInProjet(ProjectHeadManagementModel projectHeadModel)
    //    {
    //        _projectHeadModel = projectHeadModel;
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public void Execute(object parameter)
    //    {
    //        Head head = _projectHeadModel.SelectedRemainingHead;

    //        //if (head.Key > 0)
    //        //{
    //        List<Head> existingHeads = _projectHeadModel.HeadsForProject.ToList();
    //        existingHeads.Add(head);
    //        _projectHeadModel.HeadsForProject = existingHeads.ToArray();

    //        //Head removableHead = _projectHeadModel.RemainingHeads.Where(rc => rc.Key == head.Key).SingleOrDefault();
    //        List<Head> existingRemovableHeads = _projectHeadModel.RemainingHeads.ToList();
    //        //existingRemovableHeads.Remove(removableHead);
    //        _projectHeadModel.RemainingHeads = existingRemovableHeads.ToArray();

    //        _projectHeadModel.SelectedHeadForProject = head;
    //        _projectHeadModel.SelectedRemainingHead = null;
    //        //}
    //    }
    //}

    //public class RemoveHeadFromProjet : ICommand
    //{
    //    ProjectHeadManagementModel _projectHeadModel;
    //    public RemoveHeadFromProjet(ProjectHeadManagementModel projectHeadModel)
    //    {
    //        _projectHeadModel = projectHeadModel;
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public void Execute(object parameter)
    //    {
    //        Head head = _projectHeadModel.SelectedHeadForProject;

    //        //if (head.Key > 0)
    //        //{
    //        List<Head> existingHeads = _projectHeadModel.RemainingHeads.ToList();
    //        existingHeads.Add(head);
    //        _projectHeadModel.RemainingHeads = existingHeads.ToArray();

    //        //KeyValuePair<int, string> removableHead = _projectHeadModel.HeadsForProject.Where(cp => cp.Key == head.Key).SingleOrDefault();
    //        //List<KeyValuePair<int, string>> existingRemovableHeads = _projectHeadModel.HeadsForProject.ToList();
    //        //existingRemovableHeads.Remove(removableHead);
    //        //_projectHeadModel.HeadsForProject = existingRemovableHeads.ToArray();

    //        //_projectHeadModel.SelectedRemainingHead = head;
    //        //_projectHeadModel.SelectedHeadForProject = new KeyValuePair<int, string>(0, null);
    //        //}
    //    }
    //}

    //public class SaveProjectHeadRelation : ICommand
    //{
    //    ProjectHeadManagementModel _projectHeadModel;
    //    private IHeadManager _headManager;
    //    private IProjectManager _projectManager;
    //    public SaveProjectHeadRelation(ProjectHeadManagementModel model)
    //    {
    //        _projectHeadModel = model;
    //        _headManager = BLLCoreFactory.GetHeadManager();
    //        _projectManager = BLLCoreFactory.GetProjectManager();
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        return _projectHeadModel.SelectedProject != null;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public void Execute(object parameter)
    //    {
    //        MessageService.Instance.Reset();
    //        string message = "";

    //        if (_projectHeadModel.RemainingHeads != null && _projectHeadModel.RemainingHeads.Count() > 0)
    //        {
    //            IList<Head> removeHeads = _projectHeadModel.RemainingHeads;//.Select(rc => rc.Key).ToArray();
    //            int removedItems = _projectManager.RemoveHeadsFromProject(_projectHeadModel.SelectedProject,
    //                                                                      _projectHeadModel.RemainingHeads);
    //            Message removedMessage = MessageService.Instance.GetLatestMessage();
    //            if (removedItems > 0) message += removedMessage.MessageText + Environment.NewLine;
    //        }
    //        if (_projectHeadModel.HeadsForProject != null && _projectHeadModel.HeadsForProject.Count() > 0)
    //        {
    //            IList<Head> addedHeads = _projectHeadModel.HeadsForProject;//.Select(cp => cp.Key).ToArray();
    //            int addedItems = _projectManager.AddHeadsToProject(_projectHeadModel.SelectedProject, addedHeads);
    //            Message addedMessage = MessageService.Instance.GetLatestMessage();
    //            if (addedItems > 0) message += addedMessage.MessageText;
    //        }

    //        _projectHeadModel.NotificationMessage = message;
    //    }
    //}
}


