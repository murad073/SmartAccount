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
using GKS.Model.ViewModels;

namespace GKS.XAML.UserControls
{
    /// <summary>
    /// Interaction logic for ProjectHeadMgmtUC.xaml
    /// </summary>
    public partial class ProjectHeadMgmtUC : UserControl
    {
        public ProjectHeadMgmtUC()
        {
            InitializeComponent();
            DataContext = new ProjectHeadManagementModel();
        }

        //private void buttonSave_Click(object sender, RoutedEventArgs e)
        //{
        //    ProjectHeadManagementModel viewModel = (ProjectHeadManagementModel)DataContext;
        //    if (viewModel != null && viewModel.SaveProjectsForHead.CanExecute(this))
        //       viewModel.SaveProjectsForHead.Execute(this);
        //}

        //private void RefreshButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ProjectHeadManagementModel vm = (ProjectHeadManagementModel)DataContext;
        //    vm.Reset();
        //}
    }
}
