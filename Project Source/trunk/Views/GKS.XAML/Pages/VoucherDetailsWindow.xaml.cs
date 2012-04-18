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
using GKS.Model.ViewModels;

namespace GKS.XAML.Pages
{
    /// <summary>
    /// Interaction logic for VoucherDetailsWindow.xaml
    /// </summary>
    public partial class VoucherDetailsWindow : Window
    {
        public delegate void SimpleDelegate();
        public SimpleDelegate CallbackOnClose { get; set; }

        VoucherDetailsModel _vm;

        public VoucherDetailsWindow()
        {
            InitializeComponent();
            _vm = new VoucherDetailsModel();
            _vm.CloseWindow = () => { CallbackOnClose(); this.Close(); };
            DataContext = _vm;

            this.PreviewKeyDown += HandleEsc;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
