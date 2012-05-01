using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using BLL.Factories;
using BLL.Model;
using BLL.Model.Entity;
using BLL.Model.Managers;
using GKS.Factory;

namespace GKS.Model.ViewModels
{
    public class ProjectManagementModel : ViewModelBase
    {
        readonly IProjectManager _projectManager;

        public ProjectManagementModel()
        {
            _projectManager = BLLCoreFactory.GetProjectManager();
        }

        public IList<Project> Projects
        {
            get
            {
                return _projectManager.GetProjects();
            }
        }

        public void Reset()
        {
            NotifyPropertyChanged("Projects");
        }

        private Project _selectedGridItem;
        public Project SelectedGridItem
        {
            get { return _selectedGridItem; }
            set
            {
                _selectedGridItem = value;
            }
        }

        private RelayCommand _refreshButtonClicked;
        public ICommand RefreshButtonClicked
        {
            get
            {
                return _refreshButtonClicked ?? (_refreshButtonClicked = new RelayCommand(p1 => this.Reset()));
            }
        }
    }
}
