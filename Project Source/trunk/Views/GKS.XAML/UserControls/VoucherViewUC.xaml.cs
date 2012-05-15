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
using GKS.Model;
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

        private void buttonVoucherDetails_Click(object sender, RoutedEventArgs e)
        {
            VoucherViewModel vm = DataContext as VoucherViewModel;
            VoucherItem voucher = vm.SelectedVoucherItem;
            VoucherDetailsWindow voucherWindow = new VoucherDetailsWindow(voucher) { Owner = Window.GetWindow(this), CallbackOnClose = vm.Reset };
            voucherWindow.ShowDialog();
        }

        private void buttonExport_Click(object sender, RoutedEventArgs e)
        {
            if (VoucherDataGrid.Items.Count > 0)
                VoucherDataGrid.ExportToExcel();
        }
    }
}
