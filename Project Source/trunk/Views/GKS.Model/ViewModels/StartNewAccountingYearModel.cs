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
    public class StartNewFinantialYearModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        public StartNewFinantialYearModel() 
        {
            try
            {
                _projectManager = BLLCoreFactory.GetProjectManager();

                _lastFinantialYearDatagrid = new List<LastYearDatagridRow>();
                AllProjects = _projectManager.GetProjects(false);
            }
            catch
            { }
        }

        string _lastFinantialYear;
        string LastFinantialYear
        {
            get
            {
                return _lastFinantialYear;
            }
            set
            {
                _lastFinantialYear = value;
                NotifyPropertyChanged("LastFinantialYear");
            }
        }

        private string[] _newFinantialYear;
        public string[] NewFinantialYear
        {
            get
            {
                return _newFinantialYear;
            }
            set
            {
                _newFinantialYear = value;
                NotifyPropertyChanged("NewFinantialYear");
            }
        }

        private string _selectedNewFinantialYear;
        public string SelectedNewFinantialYear
        {
            get
            {
                return _selectedNewFinantialYear;
            }
            set
            {
                _selectedNewFinantialYear = value;
                NotifyPropertyChanged("SelectedNewFinantialYear");
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

        IList<LastYearDatagridRow> _lastFinantialYearDatagrid;
        IList<LastYearDatagridRow> LastFinantialYearDatagrid
        {
            get
            {
                LastYearDatagridRow temp = new LastYearDatagridRow { HeadName = "Test Head", Amount = 0 };
                _lastFinantialYearDatagrid.Add(temp);

                return _lastFinantialYearDatagrid;
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
