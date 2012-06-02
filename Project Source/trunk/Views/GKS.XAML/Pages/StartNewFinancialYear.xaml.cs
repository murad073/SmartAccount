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
    /// Interaction logic for StartNewFinancialYear.xaml
    /// </summary>
    public partial class StartNewFinancialYear : Window
    {
        StartNewFinancialYearModel _vm;
        public StartNewFinancialYear()
        {
            InitializeComponent();
            _vm = new StartNewFinancialYearModel();
            DataContext = _vm;
        }

        private void buttonEditOpeningBalances_Click(object sender, RoutedEventArgs e)
        {
            OpeningBalanceSetupWindow projectWindow = new OpeningBalanceSetupWindow() { Owner = Window.GetWindow(this) };
            projectWindow.ShowDialog();
        }
    }
}
