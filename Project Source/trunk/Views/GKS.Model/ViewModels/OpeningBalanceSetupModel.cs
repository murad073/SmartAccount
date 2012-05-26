using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Factories;

namespace GKS.Model.ViewModels
{
    public class OpeningBalanceSetupModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IHeadManager _headManager;

        public OpeningBalanceSetupModel()
        {
            try
            {
                _projectManager = BLLCoreFactory.GetProjectManager();
                _headManager = BLLCoreFactory.GetHeadManager();
                _openingBalanceDataGrid = new List<OpeningBalanceGridRow>();

                AllProjects = _projectManager.GetProjects(false);
            }
            catch
            { }
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
                NotifyPropertyChanged("SelectedProject");
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

        private string[] _openingBalanceYear;
        public string[] OpeningBalanceYear
        {
            get
            {
                return _openingBalanceYear;
            }
            set
            {
                _openingBalanceYear = value;
                NotifyPropertyChanged("OpeningBalanceYear");
            }
        }

        private string _selectedOpeningBalanceYear;
        public string SelectedOpeningBalanceYear
        {
            get
            {
                return _selectedOpeningBalanceYear;
            }
            set
            {
                _selectedOpeningBalanceYear = value;
                NotifyPropertyChanged("SelectedOpeningBalanceYear");
            }
        }

        private IList<OpeningBalanceGridRow> _openingBalanceDataGrid;
        public IList<OpeningBalanceGridRow> OpeningBalanceDataGrid
        {
            get
            {
                // Temporary code.
                OpeningBalanceGridRow gridRow = new OpeningBalanceGridRow { HeadName = "Test", CurrentYear = 0, PreviousYear = 0 };
                _openingBalanceDataGrid.Add(gridRow);

                return _openingBalanceDataGrid;
            }
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

        private RelayCommand _saveButtonClicked;
        public ICommand SaveButtonClicked
        {
            get { return _saveButtonClicked ?? (_saveButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }

        private RelayCommand _oKButtonClicked;
        public ICommand OKButtonClicked
        {
            get { return _oKButtonClicked ?? (_oKButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }
    }

    public class OpeningBalanceGridRow
    {
        public string HeadName { get; set; }
        public double CurrentYear { get; set; }
        public double PreviousYear { get; set; }
    }
}
