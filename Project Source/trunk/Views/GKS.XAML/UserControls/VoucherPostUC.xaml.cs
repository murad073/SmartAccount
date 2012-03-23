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
using BLL.Model.Schema;
using GKS.Model.ViewModels;

namespace GKS.XAML.UserControls
{
    /// <summary>
    /// Interaction logic for VoucherPost.xaml
    /// </summary>
    public partial class VoucherPostUC : UserControl
    {
        public VoucherPostUC()
        {
            InitializeComponent();
            DataContext = new VoucherPost();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            VoucherPost vm = DataContext as VoucherPost;
            vm.Reset();
        }
    }
}
