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
    /// Interaction logic for HeadMgmtUC.xaml
    /// </summary>
    public partial class HeadMgmtUC : UserControl
    {
        public HeadMgmtUC()
        {
            InitializeComponent();
            DataContext = new HeadMgmtModel();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            HeadMgmtModel vm = DataContext as HeadMgmtModel;

            AddEditHeadWindow headWindow = new AddEditHeadWindow { CallbackOnClose = vm.Reset, Owner = Window.GetWindow(this) };
            headWindow.SetOperationType(OperationType.Add);
            headWindow.ShowDialog();
        }

        private void buttonView_Click(object sender, RoutedEventArgs e)
        {
            HeadMgmtModel vm = DataContext as HeadMgmtModel;
            Head head = vm.SelectedGridItem;
            if (head != null)
            {
                AddEditHeadWindow headWindow = new AddEditHeadWindow(head) { CallbackOnClose = vm.Reset, Owner = Window.GetWindow(this) };
                headWindow.SetOperationType(OperationType.Update);
                headWindow.ShowDialog();
            }
            else MessageBox.Show("No head is selected.");
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            HeadMgmtModel vm = DataContext as HeadMgmtModel;
            vm.Reset();
        }
    }
}


