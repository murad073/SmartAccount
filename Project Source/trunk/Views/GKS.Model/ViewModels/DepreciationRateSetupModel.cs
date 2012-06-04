using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Managers;
using BLL.Factories;
using BLL.Model.Entity;
using System.Windows.Input;

namespace GKS.Model.ViewModels
{
    public class DepreciationRateSetupModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IHeadManager _headManager;

        public DepreciationRateSetupModel()
        {
            try
            {
                _projectManager = BLLCoreFactory.GetProjectManager();
                _headManager = BLLCoreFactory.GetHeadManager();
                _depreciationRateDataGrid = new List<DepreciationRateGridRow>();

                AllProjects = _projectManager.GetProjects();
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
                return _headManager.GetCapitalHeads(SelectedProject);
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

        private IList<DepreciationRateGridRow> _depreciationRateDataGrid;
        public IList<DepreciationRateGridRow> DepreciationRateDataGrid
        {
            get
            {
                // Temporary code.
                DepreciationRateGridRow gridRow = new DepreciationRateGridRow { HeadName = "Test", DepreciationRate = 0 };
                _depreciationRateDataGrid.Add(gridRow);

                return _depreciationRateDataGrid;
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

    public class DepreciationRateGridRow
    {
        public string HeadName { get; set; }
        public double DepreciationRate { get; set; }
    }
}
