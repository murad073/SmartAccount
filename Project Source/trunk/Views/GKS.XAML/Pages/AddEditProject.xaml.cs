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

namespace GKS.XAML
{
    /// <summary>
    /// Interaction logic for AddEditProject.xaml
    /// </summary>
    public partial class AddEditProject : Page
    {
        public AddEditProject()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            AddEditProjectModel viewModel = (AddEditProjectModel)DataContext;
            if (viewModel != null && viewModel.SaveButtonClicked.CanExecute(this))
                viewModel.SaveButtonClicked.Execute(this);
            //NavigationService.Navigate(new System.Uri(@"Background.xaml", UriKind.RelativeOrAbsolute));
            if (NavigationService.CanGoBack) NavigationService.GoBack();
            else NavigationService.Navigate("LoginPage.xaml");
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new System.Uri(@"Background.xaml", UriKind.RelativeOrAbsolute));
            if (NavigationService.CanGoBack) NavigationService.GoBack();
            else NavigationService.Navigate("LoginPage.xaml");
        }
    }
}

