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
    public class CloseCurrentFinantialYearModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        public CloseCurrentFinantialYearModel()
        {
            _projectManager = BLLCoreFactory.GetProjectManager();

            _currentFinantialYearDatagrid = new List<CurrentYearDatagridRow>();
            AllProjects = _projectManager.GetProjects(false);
        }

        string _currentFinantialYear;
        string CurrentFinantialYear
        {
            get
            {
                return _currentFinantialYear;
            }
            set
            {
                _currentFinantialYear = value;
                NotifyPropertyChanged("CurrentFinantialYear");
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

        IList<CurrentYearDatagridRow> _currentFinantialYearDatagrid;
        IList<CurrentYearDatagridRow> CurrentFinantialYearDatagrid
        {
            get
            {
                CurrentYearDatagridRow temp = new CurrentYearDatagridRow { HeadName = "Test Head", Amount = 0 };
                _currentFinantialYearDatagrid.Add(temp);

                return _currentFinantialYearDatagrid;
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

        private RelayCommand _closeFinantialYearClicked;
        public ICommand CloseFinantialYearClicked
        {
            get { return _closeFinantialYearClicked ?? (_closeFinantialYearClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }

        private RelayCommand _oKClicked;
        public ICommand OKClicked
        {
            get { return _oKClicked ?? (_oKClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }
    }

    public class CurrentYearDatagridRow
    {
        public string HeadName { get; set; }
        public double Amount { get; set; }
    }
}
