using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Factories;
using GKS.XAML.Reports;

namespace GKS.XAML
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FixedAssetScheduleMenuItemClick(object sender, RoutedEventArgs e)
        {
            FixedAssetSchedule fixedAssetSchedule = new FixedAssetSchedule {Owner = this};
            fixedAssetSchedule.ShowDialog();
        }
    }
}
