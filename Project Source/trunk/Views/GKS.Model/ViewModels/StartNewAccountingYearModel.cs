﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BLL.Model.Managers;
using BLL.Factories;
using BLL.Model.Entity;

namespace GKS.Model.ViewModels
{
    public class StartNewAccountingYearModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        public StartNewAccountingYearModel() 
        {
            try
            {
                _projectManager = BLLCoreFactory.GetProjectManager();

                _lastAccountingYearDatagrid = new List<LastYearDatagridRow>();
                AllProjects = _projectManager.GetProjects(false);
            }
            catch
            { }
        }

        string _lastAccountingYear;
        string LastAccountingYear
        {
            get
            {
                return _lastAccountingYear;
            }
            set
            {
                _lastAccountingYear = value;
                NotifyPropertyChanged("LastAccountingYear");
            }
        }

        private string[] _newAccountingYear;
        public string[] NewAccountingYear
        {
            get
            {
                return _newAccountingYear;
            }
            set
            {
                _newAccountingYear = value;
                NotifyPropertyChanged("NewAccountingYear");
            }
        }

        private string _selectedNewAccountingYear;
        public string SelectedNewAccountingYear
        {
            get
            {
                return _selectedNewAccountingYear;
            }
            set
            {
                _selectedNewAccountingYear = value;
                NotifyPropertyChanged("SelectedNewAccountingYear");
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

        IList<LastYearDatagridRow> _lastAccountingYearDatagrid;
        IList<LastYearDatagridRow> LastAccountingYearDatagrid
        {
            get
            {
                LastYearDatagridRow temp = new LastYearDatagridRow { HeadName = "Test Head", Amount = 0 };
                _lastAccountingYearDatagrid.Add(temp);

                return _lastAccountingYearDatagrid;
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