using System.Windows;
using System.Windows.Controls;
using BLL.Model.Entity;
using GKS.Model;
using GKS.Model.ViewModels;
using GKS.XAML.Pages;


namespace GKS.XAML.UserControls
{
    public partial class ProjectMgmtUC : UserControl
    {
        private readonly ProjectManagementModel _vm;
        public ProjectMgmtUC()
        {
            InitializeComponent();
            _vm = new ProjectManagementModel();
            DataContext = _vm;
        }

        internal void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditProjectWindow projectWindow = new AddEditProjectWindow { Owner = Window.GetWindow(this) };
            projectWindow.Closed += (sndr, eventArgs) => _vm.Reset();
            projectWindow.SetOperationType(OperationType.Add);
            projectWindow.ShowDialog();
        }

        private void buttonView_Click(object sender, RoutedEventArgs e)
        {
            Project project = _vm.SelectedGridItem;
            if (project != null)
            {
                AddEditProjectWindow projectWindow = new AddEditProjectWindow(project) { Owner = Window.GetWindow(this) };
                projectWindow.Closed += (sndr, eventArgs) => _vm.Reset();
                projectWindow.SetOperationType(OperationType.Update);
                projectWindow.ShowDialog();
            }
            else MessageBox.Show("No project is selected.");
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            //ExportToExcel<Project, Projects> s = new ExportToExcel<Project, Projects>();
            //ICollectionView view = CollectionViewSource.GetDefaultView(dataGridAllProjects.ItemsSource);
            //s.dataToPrint = (List<Project>)view.SourceCollection;
            //s.GenerateReport();
        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            TestWindow testWnd = new TestWindow();
            testWnd.ShowDialog();
        }
    }
}
