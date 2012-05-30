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
using GKS.Model;

namespace GKS.XAML.Pages
{
    /// <summary>
    /// Interaction logic for OpeningBalanceSetupWindow.xaml
    /// </summary>
    public partial class OpeningBalanceSetupWindow : Window
    {
        public OpeningBalanceSetupWindow()
        {
            InitializeComponent();
            DataContext = new OpeningBalanceSetupModel();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void buttonExport_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOpeningBalance.Items.Count > 0)
                dataGridOpeningBalance.ExportToExcel();
        }
    }
}
