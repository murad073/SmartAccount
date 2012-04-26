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
    public class ProjectManagementModel : INotifyPropertyChanged
    {
        readonly IProjectManager _projectManager;

        public ProjectManagementModel()
        {
            _projectManager = BLLCoreFactory.GetProjectManager();// new ProjectManager(GKSFactory.GetProjectRepository(), GKSFactory.GetHeadRepository(), GKSFactory.GetRecordRepository());
        }

        public IList<Project> Projects
        {
            get
            {
                return _projectManager.GetProjects();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Reset()
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Projects"));
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
    }
}
