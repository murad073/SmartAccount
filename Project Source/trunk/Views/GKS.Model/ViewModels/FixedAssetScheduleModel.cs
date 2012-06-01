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
    public class FixedAssetScheduleModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        public FixedAssetScheduleModel()
        {
            _projectManager = BLLCoreFactory.GetProjectManager();
            AllProjects = _projectManager.GetProjects(false);
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
            }
        }

        IList<FixedAssetScheduleDatagridRow> _fixedAssetScheduleDataGrid;
        IList<FixedAssetScheduleDatagridRow> FixedAssetScheduleDataGrid
        {
            get
            {
                FixedAssetScheduleDatagridRow temp = new FixedAssetScheduleDatagridRow { SlNo = 1, DetailsOfAsset = "Table1", OpeningCost = 1000, CostAddedThisYear = 50, TotalCost = 1050, DepreciationRate = 10, OpeningDepreciation = 200, DepChargedThisYear = 105, AccumulatedDep = 305, WrittenDownValue = 695 };
                _fixedAssetScheduleDataGrid.Add(temp);

                return _fixedAssetScheduleDataGrid;
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

        private RelayCommand _disposeButtonClicked;
        public ICommand DisposeButtonClicked
        {
            get { return _disposeButtonClicked ?? (_disposeButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }

        private RelayCommand _oKButtonClicked;
        public ICommand OKButtonClicked
        {
            get { return _oKButtonClicked ?? (_oKButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }
    }

    public class FixedAssetScheduleDatagridRow
    {
        public int SlNo { get; set; }
        public string DetailsOfAsset { get; set; }
        public double OpeningCost { get; set; }
        public double CostAddedThisYear { get; set; }
        public double TotalCost { get; set; }
        public double DepreciationRate { get; set; }
        public double OpeningDepreciation { get; set; }
        public double DepChargedThisYear { get; set; }
        public double AccumulatedDep { get; set; }
        public double WrittenDownValue { get; set; }
    }
}
