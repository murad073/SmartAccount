using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BLL.Model.Managers;
using BLL.Factories;
using BLL.Model.Entity;

namespace GKS.Model.ViewModels
{
    public class StartNewFinancialYearModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        public StartNewFinancialYearModel() 
        {
            try
            {
                _projectManager = BLLCoreFactory.GetProjectManager();

                _lastFinancialYearDatagrid = new List<LastYearDatagridRow>();
                AllProjects = _projectManager.GetProjects(false);
            }
            catch
            { }
        }

        string _lastFinancialYear;
        string LastFinancialYear
        {
            get
            {
                return _lastFinancialYear;
            }
            set
            {
                _lastFinancialYear = value;
                NotifyPropertyChanged("LastFinancialYear");
            }
        }

        private string[] _newFinancialYear;
        public string[] NewFinancialYear
        {
            get
            {
                return _newFinancialYear;
            }
            set
            {
                _newFinancialYear = value;
                NotifyPropertyChanged("NewFinancialYear");
            }
        }

        private string _selectedNewFinancialYear;
        public string SelectedNewFinancialYear
        {
            get
            {
                return _selectedNewFinancialYear;
            }
            set
            {
                _selectedNewFinancialYear = value;
                NotifyPropertyChanged("SelectedNewFinancialYear");
            }
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

        IList<LastYearDatagridRow> _lastFinancialYearDatagrid;
        IList<LastYearDatagridRow> LastFinancialYearDatagrid
        {
            get
            {
                LastYearDatagridRow temp = new LastYearDatagridRow { HeadName = "Test Head", Amount = 0 };
                _lastFinancialYearDatagrid.Add(temp);

                return _lastFinancialYearDatagrid;
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

        private RelayCommand _editOpeningBalanceClicked;
        public ICommand EditOpeningBalanceClicked
        {
            get { return _editOpeningBalanceClicked ?? (_editOpeningBalanceClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }

        private RelayCommand _importToCurrrentYearClicked;
        public ICommand ImportToCurrrentYearClicked
        {
            get { return _importToCurrrentYearClicked ?? (_importToCurrrentYearClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }
    }

    public class LastYearDatagridRow
    {
        public string HeadName { get; set; }
        public double Amount { get; set; }
    }
}
