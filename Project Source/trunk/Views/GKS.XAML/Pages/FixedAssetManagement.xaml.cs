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
    /// Interaction logic for FixedAssetManagement.xaml
    /// </summary>
    public partial class FixedAssetManagement : Window
    {
        public FixedAssetManagement()
        {
            InitializeComponent();
            DataContext = new FixedAssetManagementModel();
        }
    }
}
