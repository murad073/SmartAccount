using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Model.Entity;
using GKS.Model.ViewModels;

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

            AddEditProjectWindow projectWindow = new AddEditProjectWindow {Owner = Window.GetWindow(this), CallbackOnClose = vm.Reset};
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
    }
}
