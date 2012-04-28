using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GKS.Model.ViewModels;
using BLL.Model.Schema;
using System.ComponentModel;
using GKS.XAML.Pages;


namespace GKS.XAML.UserControls
{
    /// <summary>
    /// Interaction logic for ProjectMgmtUC.xaml
    /// </summary>
    public partial class ProjectMgmtUC : UserControl
    {
        public ProjectMgmtUC()
        {
            InitializeComponent();
            DataContext = new ProjectManagementModel();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            ProjectManagementModel vm = DataContext as ProjectManagementModel;

            AddEditProjectWindow projectWindow = new AddEditProjectWindow { Owner = Window.GetWindow(this), CallbackOnClose = vm.Reset };
            projectWindow.SetOperationType(OperationType.Add);
            projectWindow.ShowDialog();
        }

        private void buttonView_Click(object sender, RoutedEventArgs e)
        {
            ProjectManagementModel vm = DataContext as ProjectManagementModel;
            Project project = vm.SelectedGridItem;
            if (project != null)
            {
                AddEditProjectWindow projectWindow = new AddEditProjectWindow(project) { Owner = Window.GetWindow(this), CallbackOnClose = vm.Reset };
                projectWindow.SetOperationType(OperationType.Update);
                projectWindow.ShowDialog();
            }
            else MessageBox.Show("No project is selected.");
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectManagementModel vm = DataContext as ProjectManagementModel;
            vm.Reset();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel<Project, Projects> s = new ExportToExcel<Project, Projects>();
            ICollectionView view = CollectionViewSource.GetDefaultView(dataGridAllProjects.ItemsSource);
            s.dataToPrint = (List<Project>)view.SourceCollection;
            s.GenerateReport();
        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            TestWindow testWnd = new TestWindow();
            testWnd.ShowDialog();
        }
    }    
}
