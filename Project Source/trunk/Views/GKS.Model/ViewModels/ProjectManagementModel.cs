using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using BLL.Factories;
using BLL.Model;
using BLL.Model.Managers;
using BLL.Model.Schema;
//using BLL.ProjectManagement;
using GKS.Factory;

namespace GKS.Model.ViewModels
{
    public class ProjectManagementModel : INotifyPropertyChanged
    {
        IProjectManager _projectManager;

        public ProjectManagementModel()
        {
            _projectManager = BLLCoreFactory.GetProjectManager();// new ProjectManager(GKSFactory.GetProjectRepository(), GKSFactory.GetHeadRepository(), GKSFactory.GetRecordRepository());
            //SelectedGridItem = null;
            //AddProjectButtonClicked = new AddnewProject(this);
            //ViewProjectButtonClicked = new ViewProject(this);
        }

        //public ICommand AddProjectButtonClicked { get; set; }
        //public ICommand ViewProjectButtonClicked { get; set; }

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
            //SelectedGridItem = null;
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

    //class ViewProject : ICommand
    //{
    //    private ProjectManagementModel _projectMgmt;
    //    public ViewProject(ProjectManagementModel projectMgmt)
    //    {
    //        _projectMgmt = projectMgmt;
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public void Execute(object parameter)
    //    {

    //    }
    //}
    //class AddnewProject : ICommand
    //{
    //    private ProjectManagementModel _projectMgmt;
    //    public AddnewProject(ProjectManagementModel projectMgmt)
    //    {
    //        _projectMgmt = projectMgmt;
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public void Execute(object parameter)
    //    {
    //        //NavigationService navService = NavigationService.Equals()
    //        //navService.Navigate(new System.Uri("AddEditProjectPage.xaml", UriKind.RelativeOrAbsolute));
    //    }
    //}
    //class ProjectHeadOK : ICommand
    //{
    //    private ProjectManagementModel _projectMgmt;
    //    public ProjectHeadOK(ProjectManagementModel projectMgmt)
    //    {
    //        _projectMgmt = projectMgmt;
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public void Execute(object parameter)
    //    {
    //    }
    //}
    //class ProjectHeadCancel : ICommand
    //{
    //    private ProjectManagementModel _projectMgmt;
    //    public ProjectHeadCancel(ProjectManagementModel projectMgmt)
    //    {
    //        _projectMgmt = projectMgmt;
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public void Execute(object parameter)
    //    {
    //    }
    //}
}
