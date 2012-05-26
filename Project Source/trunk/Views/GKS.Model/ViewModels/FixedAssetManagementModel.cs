using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Factories;
using System.Windows.Input;

namespace GKS.Model.ViewModels
{
    public class FixedAssetManagementModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        public FixedAssetManagementModel()
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

        private string[] _assetTypes;
        public string[] AssetTypes
        {
            get
            {
                return _assetTypes;
            }
            set
            {
                _assetTypes = value;
                NotifyPropertyChanged("AssetTypes");
            }
        }

        private string _selectedAssetType;
        public string SelectedAssetType
        {
            get
            {
                return _selectedAssetType;
            }
            set
            {
                _selectedAssetType = value;
                NotifyPropertyChanged("SelectedAssetType");
            }
        }

        IList<FixedAssetManagementDatagridRow> _fixedAssetManagementDataGrid;
        IList<FixedAssetManagementDatagridRow> FixedAssetManagementDataGrid
        {
            get
            {
                FixedAssetManagementDatagridRow temp = new FixedAssetManagementDatagridRow { ItemName = "Test table", VoucherNo = "DV-100", OriginalCost = 100, Date = new DateTime(), DepreciationRate = 10, DepreciatedAmount = 50, RemainingAmount = 50, DisposalDate = new DateTime()};
                _fixedAssetManagementDataGrid.Add(temp);

                return _fixedAssetManagementDataGrid;
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

    public class FixedAssetManagementDatagridRow
    {
        public string ItemName { get; set; }
        public string VoucherNo { get; set; }
        public double OriginalCost { get; set; }
        public DateTime Date { get; set; }
        public double DepreciationRate { get; set; }
        public double DepreciatedAmount { get; set; }
        public double RemainingAmount { get; set; }
        public DateTime DisposalDate { get; set; }
    }
}
