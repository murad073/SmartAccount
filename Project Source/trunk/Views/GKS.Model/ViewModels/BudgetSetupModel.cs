using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Entity;
using System.Windows.Input;
using BLL.Model.Managers;
using BLL.Factories;
using BLL.Messaging;

namespace GKS.Model.ViewModels
{
    public class BudgetSetupModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IHeadManager _headManager;
        private readonly IBudgetManager _budgetManager;

        public BudgetSetupModel()
        {
            try
            {
                _projectManager = BLLCoreFactory.GetProjectManager();
                _headManager = BLLCoreFactory.GetHeadManager();
                _budgetManager = BLLCoreFactory.GetBudgetManager();
//<<<<<<< HEAD

//                _budgetDataGrid = new List<BudgetGridRow>();
//=======
//>>>>>>> githubJakaria42/master

                AllProjects = _projectManager.GetProjects();

                LoadBudgetYears();
                SelectedBudgetYear = DateTime.Now.Year;
                //NotifyBudgetDataGrid();
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

        private List<int> _budgetYear;
        public List<int> BudgetYear
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

        private double _amount;
        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                NotifyPropertyChanged("Amount");
            }
        }

        private void LoadBudgetYears()
        {
            // We'll show budgets for current year +- 10 years, total 20 years.
            List<int> years = new List<int>();
            for (int i = DateTime.Now.Year+10; i > DateTime.Now.Year-10; i--)
            {
                years.Add(i);
            }

            BudgetYear = years;
        }

        private int _selectedBudgetYear;
        public int SelectedBudgetYear
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

        //private IList<BudgetGridRow> _budgetDataGrid;
        //public IList<BudgetGridRow> BudgetDataGrid
        //{
        //    get
        //    {
        //        // Temporary code.
        //        BudgetGridRow gridRow = new BudgetGridRow { HeadName = "Test", CurrentYear = 0, PreviousYear = 0 };
        //        _budgetDataGrid.Add(gridRow);

        //        return _budgetDataGrid;
        //    }
        //}

        private IList<Budget> _budgetDataGridItems;
        private IList<Budget> BudgetDataGridItems
        {
            get
            {
                return _budgetDataGridItems;
            }
            set
            {
                _budgetDataGridItems = value;
                NotifyPropertyChanged("BudgetDataGridItems");
                NotifyPropertyChanged("BudgetDataGrid");
            }
        }

        private IList<BudgetGridRow> _budgetDataGrid;
        public IList<BudgetGridRow> BudgetDataGrid
        {
            get
            {
                if (BudgetDataGridItems == null || BudgetDataGridItems.Count == 0) return null;

                return BudgetDataGridItems.Select(b => GetBudgetGridRow(b)).ToList();
            }
        }

        private BudgetGridRow GetBudgetGridRow(Budget budget)
        {
            return new BudgetGridRow
            {
                HeadName = budget.ProjectHead.Head.Name,
                Amount = budget.Amount,
                FinancialYear = budget.FinancialYear,
            };
        }

        private string _messageText;
        public string MessageText
        {
            get { return _messageText; }
            set
            {
                _messageText = value;
                NotifyPropertyChanged("MessageText");
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
//<<<<<<< HEAD
//            get { return _saveButtonClicked ?? (_saveButtonClicked = new RelayCommand(p1 => Save())); }
//=======
            get { return _saveButtonClicked ?? (_saveButtonClicked = new RelayCommand(p1 => this.SaveBudget())); }
//>>>>>>> githubJakaria42/master
        }

        private RelayCommand _oKButtonClicked;
        public ICommand OKButtonClicked
        {
            get { return _oKButtonClicked ?? (_oKButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }

//<<<<<<< HEAD
//        private void Save()
//        {
//            //_budgetManager.Set();
//            this.InvokeOnFinish();
//=======
        private void SaveBudget()
        {
            if (_budgetManager.Set(SelectedProject, SelectedHead, Amount, SelectedBudgetYear))
                NotifyBudgetDataGrid();

            ShowMessage(MessageService.Instance.GetLatestMessage());
        }

        private void ShowMessage(Message message)
        {
            MessageText = message.MessageText;
            ColorCode = MessageService.Instance.GetColorCode(message.MessageType);
        }

        private void NotifyBudgetDataGrid()
        {
            BudgetDataGridItems = _budgetManager.GetAllBudgets();
//>>>>>>> githubJakaria42/master
        }
    }

    public class BudgetGridRow
    {
        public string HeadName { get; set; }
        public double Amount { get; set; }
        public string FinancialYear { get; set; }
    }
}
