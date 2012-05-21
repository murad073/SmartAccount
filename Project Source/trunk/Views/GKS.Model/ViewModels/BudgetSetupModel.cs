using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Entity;
using System.Windows.Input;
using BLL.Model.Managers;
using BLL.Factories;

namespace GKS.Model.ViewModels
{
    public class BudgetSetupModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;

        public BudgetSetupModel()
        {
            try
            {
                _projectManager = BLLCoreFactory.GetProjectManager();
                _budgetDataGrid = new List<BudgetGridRow>();
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
            }
        }

        private string[] _budgetYear;
        public string[] BudgetYear
        {
            get
            {
                return _budgetYear;
            }
            set
            {
                _budgetYear = value;
                NotifyPropertyChanged("BudgetYear");
            }
        }

        private string _selectedBudgetYear;
        public string SelectedBudgetYear
        {
            get
            {
                return _selectedBudgetYear;
            }
            set
            {
                _selectedBudgetYear = value;
                NotifyPropertyChanged("SelectedBudgetYear");
            }
        }

        private IList<BudgetGridRow> _budgetDataGrid;
        public IList<BudgetGridRow> BudgetDataGrid
        {
            get
            {
                // Temporary code.
                BudgetGridRow gridRow = new BudgetGridRow { HeadName = "Test", CurrentYear = 0, PreviousYear = 0 };
                _budgetDataGrid.Add(gridRow);

                return _budgetDataGrid;
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

        private RelayCommand _budgetViewButtonClicked;
        public ICommand BudgetViewButtonClicked
        {
            get { return _budgetViewButtonClicked ?? (_budgetViewButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }

        private RelayCommand _saveButtonClicked;
        public ICommand SaveButtonClicked
        {
            get { return _saveButtonClicked ?? (_saveButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }
    }

    public class BudgetGridRow
    {
        public string HeadName { get; set; }
        public double CurrentYear { get; set; }
        public double PreviousYear { get; set; }
    }
}
