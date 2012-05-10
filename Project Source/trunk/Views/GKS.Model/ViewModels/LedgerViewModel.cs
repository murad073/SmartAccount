using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BLL.Factories;
using BLL.Messaging;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using CodeFirst;

namespace GKS.Model.ViewModels
{
    public class LedgerViewModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IHeadManager _headManager;
        private readonly ILedgerManager _ledgerManager;

        public LedgerViewModel()
        {
            try
            {
                IRepository<Record> ledgerRepository = new Repository<Record>();

                _ledgerManager = BLLCoreFactory.GetLedgerManager();
                _headManager = BLLCoreFactory.GetHeadManager();
                _projectManager = BLLCoreFactory.GetProjectManager();

                AllProjects = _projectManager.GetProjects();

                IsAllHeadsEnabled = true;
                ShowAllAdvance = false;
                LedgerEndDate = DateTime.Now;
            }
            catch { }
        }

        private IList<Project> _allProjects;
        public IList<Project> AllProjects
        {
            get
            {
                return _allProjects;
            }
            set
            {
                _allProjects = value;
                NotifyPropertyChanged("AllProjects");
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
                NotifyPropertyChanged("AllHeads");
                SelectedHead = null;
            }
        }

        public IList<Head> AllHeads
        {
            get
            {
                if (SelectedProject == null) return new List<Head>();
                return _headManager.GetHeads(SelectedProject);
            }
        }

        private bool _isAllHeadsEnabled;
        public bool IsAllHeadsEnabled
        {
            get { return _isAllHeadsEnabled; }
            set
            {
                _isAllHeadsEnabled = value;
                NotifyPropertyChanged("IsAllHeadsEnabled");
            }
        }

        private Head _selectedHead;
        public Head SelectedHead
        {
            get
            {
                return _selectedHead;
            }
            set
            {
                _selectedHead = value;
                NotifyPropertyChanged("SelectedHead");
            }
        }

        private bool _showAllAdvance;
        public bool ShowAllAdvance
        {
            get
            {
                return _showAllAdvance;
            }
            set
            {
                _showAllAdvance = value;
                NotifyPropertyChanged("ShowAllAdvance");
                SetAllHeadsIsEnabled();
            }
        }

        private DateTime _ledgerEndDate;
        public DateTime LedgerEndDate
        {
            get { return _ledgerEndDate; }
            set
            {
                _ledgerEndDate = value;
                NotifyPropertyChanged("LedgerEndDate");
            }
        }

        private IList<Record> _ledgerGridViewItems;
        public IList<Record> LedgerGridViewItems
        {
            get
            {
                return _ledgerGridViewItems;
            }
            set { _ledgerGridViewItems = value; NotifyPropertyChanged("LedgerGridViewItems"); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }

        private string _colorCode;
        public string ColorCode
        {
            get { return _colorCode; }
            private set
            {
                _colorCode = value;
                NotifyPropertyChanged("ColorCode");
            }
        }

        public void ClearMessage()
        {
            Message message = new Message();
            ColorCode = MessageService.Instance.GetColorCode(message.MessageType);
            ErrorMessage = message.MessageText;
        }

        private void SetAllHeadsIsEnabled()
        {
            if (ShowAllAdvance)
            {
                SelectedHead = null;
                IsAllHeadsEnabled = false;
            }
            else IsAllHeadsEnabled = true;
        }

        #region Relay Commands

        private RelayCommand _ledgerViewButtonClicked;
        public ICommand LedgerViewButtonClicked
        {
            get
            {
                return _ledgerViewButtonClicked ??
                       (_ledgerViewButtonClicked = new RelayCommand(p1 => this.NotifyLedgerGrid()));
            }
        }

        private RelayCommand _refreshButtonClicked;
        public ICommand RefreshButtonClicked
        {
            get { return _refreshButtonClicked ?? (_refreshButtonClicked = new RelayCommand(p1 => this.Reset())); }
        }

        private void NotifyLedgerGrid()
        {
            if (!_ledgerManager.Validate(SelectedProject, SelectedHead, ShowAllAdvance))
            {
                Message latestMessage = MessageService.Instance.GetLatestMessage();
                ErrorMessage = latestMessage.MessageText;
                ColorCode = MessageService.Instance.GetColorCode(latestMessage.MessageType);
                return;
            }
            ClearMessage();

            _ledgerManager.LedgerEndDate = LedgerEndDate;
            if (!ShowAllAdvance)
            {
                LedgerGridViewItems = _ledgerManager.GetLedgerBook(SelectedProject, SelectedHead).ToList();
            }
            else
            {
                LedgerGridViewItems = _ledgerManager.GetAllAdvance(SelectedProject);
            }
        }

        private void Reset()
        {
            AllProjects = _projectManager.GetProjects();
        }

        #endregion
    }
}
