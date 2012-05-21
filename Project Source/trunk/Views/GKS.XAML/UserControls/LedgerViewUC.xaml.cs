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
using GKS.Model;

namespace GKS.XAML.UserControls
{
    public partial class LedgerViewUC : UserControl
    {
        public LedgerViewUC()
        {
            InitializeComponent();
            DataContext = new LedgerViewModel();
        }

        private void buttonExport_Click(object sender, RoutedEventArgs e)
        {
            if (LedgerDataGrid.Items.Count > 0)
                LedgerDataGrid.ExportToExcel();
        }
    }
}
