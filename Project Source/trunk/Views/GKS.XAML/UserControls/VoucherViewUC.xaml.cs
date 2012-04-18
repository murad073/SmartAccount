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
using GKS.XAML.Pages;

namespace GKS.XAML.UserControls
{
    /// <summary>
    /// Interaction logic for VoucherViewUC.xaml
    /// </summary>
    public partial class VoucherViewUC : UserControl
    {
        public VoucherViewUC()
        {
            InitializeComponent();
            DataContext = new VoucherViewModel();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            VoucherViewModel vm = DataContext as VoucherViewModel;
            vm.Reset();
        }

        private void buttonVoucherDetails_Click(object sender, RoutedEventArgs e)
        {
            VoucherViewModel vm = DataContext as VoucherViewModel;
            //Voucher voucher = vm.SelectedGridItem;
            VoucherDetailsWindow voucherWindow = new VoucherDetailsWindow { Owner = Window.GetWindow(this), CallbackOnClose = vm.Reset };
            voucherWindow.ShowDialog();
        }
    }
}
