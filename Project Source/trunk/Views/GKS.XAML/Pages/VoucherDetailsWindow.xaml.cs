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
using System.Windows.Shapes;
using GKS.Model;
using GKS.Model.ViewModels;

namespace GKS.XAML.Pages
{
    public partial class VoucherDetailsWindow : Window
    {
        private VoucherDetailsModel _vm;

        private void Init()
        {
            InitializeComponent();
            _vm = new VoucherDetailsModel();
            _vm.OnFinish += (sndr, eventArgs) => this.Close();
            DataContext = _vm;
            this.PreviewKeyDown += HandleEsc;
        }

        public VoucherDetailsWindow(VoucherItem voucher)
        {
            Init();
            _vm.VoucherItem = voucher;
            _vm.SetRecordItems();

        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
