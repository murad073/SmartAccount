using System.Collections.Generic;
using System.ComponentModel;
using BLL.Factories;
using BLL.Model.Managers;
using BLL.Model.Schema;

namespace GKS.Model.ViewModels
{
    public class ProjectManagementModel : INotifyPropertyChanged
    {
        IProjectManager _projectManager;

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
